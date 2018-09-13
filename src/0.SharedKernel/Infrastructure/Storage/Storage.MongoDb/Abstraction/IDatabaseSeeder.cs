using System.Threading.Tasks;

namespace NM.Storage.MongoDb.Abstraction
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
