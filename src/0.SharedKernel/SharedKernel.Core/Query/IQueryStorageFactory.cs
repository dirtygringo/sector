using NM.SharedKernel.Core.Storage;

namespace NM.SharedKernel.Core.Query
{
    public interface IQueryStorageFactory<TReadEntity> : IStorageFactory<IQueryStorage<TReadEntity>> where TReadEntity : class, IQueryEntity { }
}
