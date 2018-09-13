using System;

namespace NM.SharedKernel.Core.Abstraction.Messages
{
    public interface IMessage
    {
        Guid AggregateId { get; }
        DateTime CreatedAt { get; }
    }
}
