using System;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.SharedKernel.Infrastructure.Bus
{
    public interface IBusConfiguration : IDisposable
    {
        IBusConfiguration SubscribeToEvent<TEvent>() where TEvent : class, IEvent;
        IBusConfiguration SubscribeToCommand<TCommand>() where TCommand : class, ICommand;
    }
}
