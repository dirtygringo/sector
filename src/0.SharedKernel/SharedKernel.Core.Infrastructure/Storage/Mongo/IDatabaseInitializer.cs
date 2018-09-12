using System.Threading.Tasks;

namespace NM.SharedKernel.Core.Infrastructure.Storage.Mongo
{
    internal interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
