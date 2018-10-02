using System;
using System.Linq;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Query;

namespace NM.SharedKernel.Core.Storage.Query
{
    public interface IQueryStorage<TQueryEntity> : IStorage where TQueryEntity : class, IQueryEntity
    {
        Task<IQueryable<TQueryEntity>> QueryAsync();
        Task<TQueryEntity> FindAsync(Guid id);
        Task SaveAsync(TQueryEntity entity);
    }
}
