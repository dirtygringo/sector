using NM.SharedKernel.Core.Abstraction.EventSourcing;

namespace NM.Storage.Abstraction.Event
{
    public interface IEventStorageFactory<in TEventSourced> : IStorageFactory<IEventStorage<TEventSourced>> where TEventSourced : class, IEventSourced { }
}
