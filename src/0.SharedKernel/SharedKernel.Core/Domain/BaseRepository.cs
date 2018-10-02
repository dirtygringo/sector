using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NM.SharedKernel.Core.Abstraction.EventSourcing;
using NM.SharedKernel.Core.Abstraction.Exceptions;
using NM.SharedKernel.Core.Abstraction.Helpers;
using NM.SharedKernel.Core.Abstraction.Storage.Event;

namespace NM.SharedKernel.Core.Abstraction.Domain
{
    public abstract class BaseRepository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot, IEventSourced
    {
        #region Fields

        protected readonly ILogger _logger;
        protected readonly IEventStorageFactory<TAggregateRoot> _eventStorageFactory;

        #endregion

        #region Constructor

        protected BaseRepository(ILogger<BaseRepository<TAggregateRoot>> logger, IEventStorageFactory<TAggregateRoot> eventStorageFactory)
        {
            _logger = logger;
            _eventStorageFactory = eventStorageFactory;
        }

        #endregion

        #region Methods

        public virtual async Task<TAggregateRoot> FindAsync(Guid aggregateId)
        {
            using (var eventStorage = _eventStorageFactory.Storage)
            {
                if (!await eventStorage.AggregateExistsAsync(aggregateId))
                    throw new AggregateNotFoundException($"Aggregate with id {aggregateId} does not exist.");

                var events = await eventStorage.GetAggregateEventsAsync(aggregateId);
                var aggregate = ConstructorInitializer.Construct<TAggregateRoot>(
                    new Type[] {aggregateId.GetType(), events.GetType()}, new object[] {aggregateId, events});
                return aggregate;
            }
        }

        public virtual async Task SaveAsync(TAggregateRoot aggregate)
        {
            try
            {
                using (var eventStorage = _eventStorageFactory.Storage)
                {
                    await eventStorage.SaveEventsAsync(aggregate);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    _logger.LogError(ex.Message, ex);
                }
                throw;
            }
        }

        #endregion
    }
}
