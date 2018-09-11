using System.Threading.Tasks;

namespace NM.SharedKernel.Implementation.Storage.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
