using NM.SharedKernel.Core.Storage;

namespace NM.SharedKernel.Core.EventSourcing
{
    public interface IEventStorageFactory<in TEventSourced> : IStorageFactory<IEventStorage<TEventSourced>> where TEventSourced : class, IEventSourced { }
}
