using NM.SharedKernel.Infrastructure.Storage;

namespace NM.SharedKernel.Infrastructure.EventSourcing
{
    public interface IEventStorageFactory<in TEventSourced> : IStorageFactory<IEventStorage<TEventSourced>> where TEventSourced : IEventSourced { }
}
