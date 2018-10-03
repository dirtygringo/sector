using NM.SharedKernel.Core.Bindings;

namespace NM.SharedKernel.Core.Storage.Standard
{
    public interface IStandardStorageFactory<TEntity> : IStorageFactory<IStandardStorage<TEntity>> where TEntity : class, IEntity { }
}
