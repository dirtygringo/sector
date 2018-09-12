using System.Threading.Tasks;

namespace NM.SharedKernel.Core.Infrastructure.Storage.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
