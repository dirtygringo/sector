using System;
using System.Collections.Generic;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.SharedKernel.Infrastructure.EventSourcing
{
    public interface IEventSourced
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IDomainEvent> UncomittedChanges { get; }
    }
}
