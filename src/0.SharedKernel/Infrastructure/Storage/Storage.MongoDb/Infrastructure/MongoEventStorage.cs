using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NM.SharedKernel.Core.Bindings;
using NM.SharedKernel.Core.Messages;
using NM.SharedKernel.Core.Storage.Event;
using NM.Storage.MongoDb.Format;

namespace NM.Storage.MongoDb.Infrastructure
{
    internal class MongoEventStorage<TEventSourced> : IEventStorage<TEventSourced> where TEventSourced : class, IEventSourced
    {
        #region Fields

        private bool _disposed = false;
        private readonly ILogger _logger;
        private readonly IMongoDatabase _database;
        private readonly IMessageClient _bus;

        #endregion

        #region Constructor

        public MongoEventStorage(ILogger<MongoEventStorage<TEventSourced>> logger, IMongoDatabase database, IMessageClient bus)
        {
            _logger = logger;
            _database = database;
            _bus = bus;
        }

        #endregion

        #region Properties

        private IMongoCollection<EventData> Collection => _database.GetCollection<EventData>(nameof(EventData));

        #endregion

        #region Methods

        public Task<bool> AggregateExistsAsync(Guid aggregateId)
        {
            try
            {
                return Collection.AsQueryable().AnyAsync(x => x.AggregateId == aggregateId);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public async Task<IEnumerable<IDomainEvent>> GetAggregateEventsAsync(Guid aggregateId)
        {
            try
            {
                var aggregate = await Collection.AsQueryable().SingleOrDefaultAsync(x => x.AggregateId == aggregateId);
                return aggregate?.DeserializeEvents();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public async Task SaveEventsAsync(TEventSourced source)
        {
            try
            {
                if (!Collection.AsQueryable().Any(x => x.AggregateId == source.Id))
                {
                    await Collection.InsertOneAsync(source.ToEventData());
                }
                else
                {
                    var eventData = Collection.AsQueryable().Single(x => x.AggregateId == source.Id);
                    eventData.AppendEvents(source);
                    
                    var filter = Builders<EventData>.Filter.Eq(x => x.AggregateId, source.Id);
                    var update = Builders<EventData>.Update.Set(x => x.Events, eventData.Events);
                    await Collection.UpdateOneAsync(filter, update);
                }

                foreach (var @event in source.UncomittedChanges)
                {
                    if (@event is IEvent e)
                        await _bus.PublishAsync((dynamic)e);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        private void LogException(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                _logger.LogError(ex.Message, ex);
            }
        }

        #region Disposing

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _bus.Dispose();
                }
            }
            _disposed = true;
        }

        ~MongoEventStorage()
        {
            Dispose(false);
        }

        #endregion

        #endregion
    }
}
