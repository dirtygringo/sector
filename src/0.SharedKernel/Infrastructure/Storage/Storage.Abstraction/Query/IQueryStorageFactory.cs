using NM.SharedKernel.Core.Abstraction.Query;

namespace NM.Storage.Abstraction.Query
{
    public interface IQueryStorageFactory<TReadEntity> : IStorageFactory<IQueryStorage<TReadEntity>> where TReadEntity : class, IQueryEntity { }
}
