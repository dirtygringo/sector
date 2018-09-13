using System;
using System.Threading.Tasks;

namespace NM.SharedKernel.Core.Abstraction.Domain
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot: AggregateRoot
    {
        Task SaveAsync(TAggregateRoot aggregate);
        Task<TAggregateRoot> FindAsync(Guid aggregateId);
    }
}
