using System;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Bus
{
    public interface IBusClient : IDisposable
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
