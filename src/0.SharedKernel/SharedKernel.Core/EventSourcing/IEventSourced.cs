using System;
using System.Collections.Generic;
using NM.SharedKernel.Core.Abstraction.Messages;

namespace NM.SharedKernel.Core.Abstraction.EventSourcing
{
    public interface IEventSourced
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IDomainEvent> UncomittedChanges { get; }
    }
}
