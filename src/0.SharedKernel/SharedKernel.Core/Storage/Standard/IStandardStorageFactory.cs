using NM.SharedKernel.Core.Query;

namespace NM.SharedKernel.Core.Storage.Standard
{
    public interface IStandardStorageFactory<TReadEntity> : IStorageFactory<IStandardStorage<TReadEntity>> where TReadEntity : class, IQueryEntity { }
}
