namespace NM.SharedKernel.Core.Abstraction.Storage
{
    public interface IStorageFactory<out TStorage> where TStorage : IStorage
    {
        TStorage Storage { get; }
    }
}
