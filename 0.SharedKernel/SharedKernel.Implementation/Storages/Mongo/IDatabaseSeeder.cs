using System.Threading.Tasks;

namespace NM.SharedKernel.Implementation.Storages.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
