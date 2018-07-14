using System;
using System.Threading.Tasks;

namespace NM.SharedKernel.Infrastructure.Domain
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot: AggregateRoot
    {
        Task SaveAsync(TAggregateRoot aggregate);
        TAggregateRoot FindAsync(Guid aggregateId);
    }
}
