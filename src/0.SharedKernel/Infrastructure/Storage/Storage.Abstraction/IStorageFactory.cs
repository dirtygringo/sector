namespace NM.Storage.Abstraction
{
    public interface IStorageFactory<out TStorage> where TStorage : IStorage
    {
        TStorage Storage { get; }
    }
}
