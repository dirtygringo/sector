using System.Threading.Tasks;

namespace NM.SharedKernel.Implementation.Storage.Mongo
{
    internal interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
