using System;

namespace NM.SharedKernel.Core.Messages
{
    public interface IMessageConfiguration : IDisposable
    {
        IMessageConfiguration SubscribeToEvent<TEvent>() where TEvent : class, IEvent;
        IMessageConfiguration SubscribeToCommand<TCommand>() where TCommand : class, ICommand;
    }
}
