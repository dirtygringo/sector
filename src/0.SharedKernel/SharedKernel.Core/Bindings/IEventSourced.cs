using System.Collections.Generic;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Bindings
{
    public interface IEventSourced : IEntity
    {
        int Version { get; }
        IEnumerable<IDomainEvent> UncomittedChanges { get; }
    }
}
