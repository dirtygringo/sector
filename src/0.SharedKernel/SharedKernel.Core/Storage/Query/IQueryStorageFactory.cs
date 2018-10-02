using NM.SharedKernel.Core.Query;

namespace NM.SharedKernel.Core.Storage.Query
{
    public interface IQueryStorageFactory<TReadEntity> : IStorageFactory<IQueryStorage<TReadEntity>> where TReadEntity : class, IQueryEntity { }
}
