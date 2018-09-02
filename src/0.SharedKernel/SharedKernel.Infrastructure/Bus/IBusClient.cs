using System;
using System.Threading.Tasks;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.SharedKernel.Infrastructure.Bus
{
    public interface IBusClient : IDisposable
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
