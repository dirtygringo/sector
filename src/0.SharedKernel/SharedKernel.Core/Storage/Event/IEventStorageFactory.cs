using NM.SharedKernel.Core.Bindings;

namespace NM.SharedKernel.Core.Storage.Event
{
    public interface IEventStorageFactory<in TEventSourced> : IStorageFactory<IEventStorage<TEventSourced>> where TEventSourced : class, IEventSourced { }
}
