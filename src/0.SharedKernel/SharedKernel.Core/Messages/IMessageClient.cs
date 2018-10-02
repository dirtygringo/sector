using System;
using System.Threading.Tasks;

namespace NM.SharedKernel.Core.Abstraction.Messages
{
    public interface IMessageClient : IDisposable
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
