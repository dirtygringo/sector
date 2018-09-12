namespace NM.SharedKernel.Infrastructure.Storage
{
    public interface IStorageFactory<out TStorage> where TStorage : IStorage
    {
        TStorage Storage { get; }
    }
}
