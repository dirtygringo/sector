using System;

namespace NM.SharedKernel.Core.Messages
{
    public interface IMessage
    {
        Guid AggregateId { get; }
        DateTime CreatedAt { get; }
    }
}
