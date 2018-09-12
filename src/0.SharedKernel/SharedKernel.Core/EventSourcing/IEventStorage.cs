using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Messages;
using NM.SharedKernel.Core.Storage;

namespace NM.SharedKernel.Core.EventSourcing
{
    public interface IEventStorage<in TEventSourced> : IStorage where TEventSourced: IEventSourced
    {
        Task<bool> AggregateExistsAsync(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetAggregateEventsAsync(Guid aggregateId);
        Task SaveEventsAsync(TEventSourced source);
    }
}
