using System;
using System.Linq;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Storage;

namespace NM.SharedKernel.Core.Query
{
    public interface IQueryStorage<TReadEntity> : IStorage where TReadEntity : class, IQueryEntity
    {
        Task<IQueryable<TReadEntity>> QueryAsync();
        Task<TReadEntity> FindAsync(Guid id);
        Task SaveAsync(TReadEntity entity);
    }
}
