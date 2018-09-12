using System;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Bus
{
    public interface IBusConfiguration : IDisposable
    {
        IBusConfiguration SubscribeToEvent<TEvent>() where TEvent : class, IEvent;
        IBusConfiguration SubscribeToCommand<TCommand>() where TCommand : class, ICommand;
    }
}
