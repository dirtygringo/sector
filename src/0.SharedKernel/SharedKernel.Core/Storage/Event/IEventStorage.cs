using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Bindings;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Storage.Event
{
    public interface IEventStorage<in TEventSourced> : IStorage where TEventSourced: IEventSourced
    {
        Task<bool> AggregateExistsAsync(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetAggregateEventsAsync(Guid aggregateId);
        Task SaveEventsAsync(TEventSourced source);
    }
}
