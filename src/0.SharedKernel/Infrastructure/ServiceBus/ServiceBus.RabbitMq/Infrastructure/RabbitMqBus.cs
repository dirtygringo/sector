using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using NM.SharedKernel.Core.Messages;

namespace NM.ServiceBus.RabbitMq.Infrastructure
{
    internal class RabbitMqBus : IMessageClient
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IBus _bus;

        private bool _disposed = default;

        #endregion

        #region Constructor

        public RabbitMqBus(ILogger<RabbitMqBus> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        #endregion

        #region Methods

        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            try
            {
                return _bus.PublishAsync(@event);
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

        public Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            try
            {
                return _bus.SendAsync(typeof(TCommand).AssemblyQualifiedName, command);
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

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _bus.Dispose();
            }
            _disposed = true;
        }

        ~RabbitMqBus()
        {
            Dispose(false);
        }

        #endregion

        #endregion
    }
}
