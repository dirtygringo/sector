using System;
using NM.ServiceBus.Abstraction;

namespace NM.ServiceBus.RabbitMq
{
    internal class RabbitMqListener
    {
        #region Fields

        private readonly IBusConfiguration _busConfiguration;

        #endregion

        #region Constructor

        public RabbitMqListener(IBusConfiguration busConfiguration)
        {
            _busConfiguration = busConfiguration;
        }

        #endregion

        #region Methods

        public void Register(Action<IBusConfiguration> config)
        {
            config?.Invoke(_busConfiguration);
        }

        public void Unregister()
        {
            _busConfiguration?.Dispose();
        }

        #endregion
    }
}
