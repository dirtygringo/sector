using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Abstraction.Messages;
using NM.SharedKernel.Core.Abstraction.Storage;

namespace NM.SharedKernel.Core.Abstraction.EventSourcing
{
    public interface IEventStorage<in TEventSourced> : IStorage where TEventSourced: IEventSourced
    {
        Task<bool> AggregateExistsAsync(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetAggregateEventsAsync(Guid aggregateId);
        Task SaveEventsAsync(TEventSourced source);
    }
}
