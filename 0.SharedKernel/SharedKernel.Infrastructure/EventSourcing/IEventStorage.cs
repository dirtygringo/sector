using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NM.SharedKernel.Infrastructure.Messages;
using NM.SharedKernel.Infrastructure.Storage;

namespace NM.SharedKernel.Infrastructure.EventSourcing
{
    public interface IEventStorage<in TEventSourced> : IStorage where TEventSourced: IEventSourced
    {
        Task<bool> AggregateExistsAsync(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetAggregateEventsAsync(Guid aggregateId);
        Task SaveEventsAsync(TEventSourced source);
    }
}
