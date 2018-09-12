using System;
using System.Collections.Generic;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.EventSourcing
{
    public interface IEventSourced
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IDomainEvent> UncomittedChanges { get; }
    }
}
