using System;
using NM.SharedKernel.Core.Messages;

namespace NM.ServiceBus.RabbitMq.Infrastructure
{
    internal class RabbitMqListener
    {
        #region Fields

        private readonly IMessageConfiguration _busConfiguration;

        #endregion

        #region Constructor

        public RabbitMqListener(IMessageConfiguration busConfiguration)
        {
            _busConfiguration = busConfiguration;
        }

        #endregion

        #region Methods

        public void Register(Action<IMessageConfiguration> config)
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
