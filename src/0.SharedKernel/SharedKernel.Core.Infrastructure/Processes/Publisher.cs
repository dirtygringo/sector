using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Core.Messages;
using NM.SharedKernel.Core.Processes;

namespace NM.SharedKernel.Core.Infrastructure.Processes
{
    internal class Publisher : IPublisher
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        public Publisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Methods

        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            if(@event == null) throw new ArgumentException("Event cannot be null.");

            return Task.WhenAll(_serviceProvider.GetServices<IMessageHandler<TEvent>>().AsParallel()
                .Select(handlers => handlers.HandleAsync(@event)));
        }

        #endregion
    }
}
