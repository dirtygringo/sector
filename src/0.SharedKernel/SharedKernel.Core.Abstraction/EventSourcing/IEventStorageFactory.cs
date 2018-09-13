using NM.SharedKernel.Core.Abstraction.Storage;

namespace NM.SharedKernel.Core.Abstraction.EventSourcing
{
    public interface IEventStorageFactory<in TEventSourced> : IStorageFactory<IEventStorage<TEventSourced>> where TEventSourced : class, IEventSourced { }
}
