using System;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.ServiceBus.Abstraction
{
    public interface IBusConfiguration : IDisposable
    {
        IBusConfiguration SubscribeToEvent<TEvent>() where TEvent : class, IEvent;
        IBusConfiguration SubscribeToCommand<TCommand>() where TCommand : class, ICommand;
    }
}
