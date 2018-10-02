using NM.SharedKernel.Core.Abstraction.Query;

namespace NM.SharedKernel.Core.Abstraction.Storage.Query
{
    public interface IQueryStorageFactory<TReadEntity> : IStorageFactory<IQueryStorage<TReadEntity>> where TReadEntity : class, IQueryEntity { }
}
