using System;
using NM.SharedKernel.Core.Bindings;

namespace NM.Sector.Services.Identity.ViewModels.QueryModels
{
    public class UserReadOnly : IEntity
    {
        private UserReadOnly(Guid id, string firstName, string lastName, string email, string passwordHash, string passwordSalt, DateTime createdAt)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            CreatedAt = createdAt;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public string PasswordSalt { get; }
        public DateTime CreatedAt { get; }
    }
}
