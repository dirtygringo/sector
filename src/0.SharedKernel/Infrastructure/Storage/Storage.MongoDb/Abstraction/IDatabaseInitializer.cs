using System.Threading.Tasks;

namespace NM.Storage.MongoDb.Abstraction
{
    internal interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
