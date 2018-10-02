using NM.SharedKernel.Core.Abstraction.EventSourcing;

namespace NM.SharedKernel.Core.Abstraction.Storage.Event
{
    public interface IEventStorageFactory<in TEventSourced> : IStorageFactory<IEventStorage<TEventSourced>> where TEventSourced : class, IEventSourced { }
}
