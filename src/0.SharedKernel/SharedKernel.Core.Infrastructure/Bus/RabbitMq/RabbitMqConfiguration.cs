using System;
using System.Collections.Generic;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using NM.SharedKernel.Core.Bus;
using NM.SharedKernel.Core.Messages;
using NM.SharedKernel.Core.Processes;

namespace NM.SharedKernel.Core.Infrastructure.Bus.RabbitMq
{
    internal class RabbitMqConfiguration : IBusConfiguration
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IBus _bus;
        private readonly IPublisher _publisher;
        private readonly ISender _sender;

        private bool _disposed = default;
        private readonly List<ISubscriptionResult> _subscriptions;
        private readonly List<IDisposable> _receivers;

        #endregion

        #region Constructor

        public RabbitMqConfiguration(ILogger<RabbitMqConfiguration> logger, IBus bus, IPublisher publisher, ISender sender)
        {
            _logger = logger;
            _bus = bus;
            _publisher = publisher;
            _sender = sender;
            _subscriptions = new List<ISubscriptionResult>();
            _receivers = new List<IDisposable>();
        }

        #endregion

        #region Methods

        public IBusConfiguration SubscribeToEvent<TEvent>() where TEvent : class, IEvent
        {
            var subscription = _bus.SubscribeAsync<TEvent>(typeof(TEvent).AssemblyQualifiedName, @event =>
            {
                try
                {
                    return _publisher.PublishAsync(@event);
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
            });
            _subscriptions.Add(subscription);
            return this;
        }

        public IBusConfiguration SubscribeToCommand<TCommand>() where TCommand : class, ICommand
        {
            var receiver = _bus.Receive(typeof(TCommand).AssemblyQualifiedName, handler => handler.Add<TCommand>(c => _sender.SendAsync(c)));
            _receivers.Add(receiver);
            return this;
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
                _subscriptions.ForEach(s => s.Dispose());
                _receivers.ForEach(r => r.Dispose());
                _bus.Dispose();
            }
            _disposed = true;
        }

        ~RabbitMqConfiguration()
        {
            Dispose(false);
        }

        #endregion

        #endregion
    }
}
