using System.Threading.Tasks;

namespace NM.SharedKernel.Implementation.Storages.Mongo
{
    internal interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
