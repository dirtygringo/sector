using System;

namespace NM.SharedKernel.Infrastructure.Messages
{
    public interface IMessage
    {
        Guid AggregateId { get; }
        DateTime CreatedAt { get; }
    }
}
