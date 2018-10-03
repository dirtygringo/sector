using System;
using System.Linq;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Bindings;

namespace NM.SharedKernel.Core.Storage.Standard
{
    public interface IStandardStorage<TQueryEntity> : IStorage where TQueryEntity : class, IEntity
    {
        Task<IQueryable<TQueryEntity>> QueryAsync();
        Task<TQueryEntity> FindAsync(Guid id);
        Task SaveAsync(TQueryEntity entity);
    }
}
