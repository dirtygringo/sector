using NM.SharedKernel.Core.EventSourcing;

namespace NM.SharedKernel.Core.Storage.Event
{
    public interface IEventStorageFactory<in TEventSourced> : IStorageFactory<IEventStorage<TEventSourced>> where TEventSourced : class, IEventSourced { }
}
