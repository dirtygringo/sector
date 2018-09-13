using System;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.ServiceBus.Abstraction
{
    public interface IBusClient : IDisposable
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
