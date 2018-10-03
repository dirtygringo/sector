using System;
using System.Linq;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Query;

namespace NM.SharedKernel.Core.Storage.Standard
{
    public interface IStandardStorage<TQueryEntity> : IStorage where TQueryEntity : class, IQueryEntity
    {
        Task<IQueryable<TQueryEntity>> QueryAsync();
        Task<TQueryEntity> FindAsync(Guid id);
        Task SaveAsync(TQueryEntity entity);
    }
}
