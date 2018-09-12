namespace NM.SharedKernel.Core.Storage
{
    public interface IStorageFactory<out TStorage> where TStorage : IStorage
    {
        TStorage Storage { get; }
    }
}
