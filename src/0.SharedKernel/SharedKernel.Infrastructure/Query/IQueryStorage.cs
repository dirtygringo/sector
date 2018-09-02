using System;
using System.Linq;
using System.Threading.Tasks;
using NM.SharedKernel.Infrastructure.Storage;

namespace NM.SharedKernel.Infrastructure.Query
{
    public interface IQueryStorage<TReadEntity> : IStorage where TReadEntity : class, IQueryEntity
    {
        Task<IQueryable<TReadEntity>> QueryAsync();
        Task<TReadEntity> FindAsync(Guid id);
        Task SaveAsync(TReadEntity entity);
    }
}
