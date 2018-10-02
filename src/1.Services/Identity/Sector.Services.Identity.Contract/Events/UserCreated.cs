using System;
using NM.SharedKernel.Core.Messages;

namespace NM.Sector.Services.Identity.Contract.Events
{
    public class UserCreated : IEvent
    {
        public UserCreated(Guid aggregateId, string firstName, string lastName, string email, string passwordHash, string passwordSalt, DateTime createdAt)
        {
            AggregateId = aggregateId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            CreatedAt = createdAt;
        }
        public Guid AggregateId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public string PasswordSalt { get; }
        public DateTime CreatedAt { get; }
    }
}
