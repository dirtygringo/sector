using NM.SharedKernel.Infrastructure.Storage;

namespace NM.SharedKernel.Infrastructure.Query
{
    public interface IQueryStorageFactory<TReadEntity> : IStorageFactory<IQueryStorage<TReadEntity>> where TReadEntity : class, IQueryEntity { }
}
