namespace NM.SharedKernel.Infrastructure.Storage
{
    public interface IStorageFactory<out T> where T : IStorage
    {
        T Storage { get; }
    }
}
