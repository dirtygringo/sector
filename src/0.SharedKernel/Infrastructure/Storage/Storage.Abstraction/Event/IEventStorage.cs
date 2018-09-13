using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NM.SharedKernel.Core.Abstraction.EventSourcing;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.Storage.Abstraction.Event
{
    public interface IEventStorage<in TEventSourced> : IStorage where TEventSourced: IEventSourced
    {
        Task<bool> AggregateExistsAsync(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetAggregateEventsAsync(Guid aggregateId);
        Task SaveEventsAsync(TEventSourced source);
    }
}
