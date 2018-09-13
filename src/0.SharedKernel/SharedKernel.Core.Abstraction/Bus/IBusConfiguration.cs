using System;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.SharedKernel.Core.Abstraction.Bus
{
    public interface IBusConfiguration : IDisposable
    {
        IBusConfiguration SubscribeToEvent<TEvent>() where TEvent : class, IEvent;
        IBusConfiguration SubscribeToCommand<TCommand>() where TCommand : class, ICommand;
    }
}
