using NM.SharedKernel.Core.Abstraction.Storage;

namespace NM.SharedKernel.Core.Abstraction.Query
{
    public interface IQueryStorageFactory<TReadEntity> : IStorageFactory<IQueryStorage<TReadEntity>> where TReadEntity : class, IQueryEntity { }
}
