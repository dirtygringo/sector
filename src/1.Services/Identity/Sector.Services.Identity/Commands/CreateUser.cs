using System;
using NM.SharedKernel.Core.Helpers;
using NM.SharedKernel.Core.Messages;

namespace NM.Sector.Services.Identity.Commands
{
    internal class CreateUser : ICommand
    {
        public CreateUser(string firstName, string lastName, string email, string password)
        {
            AggregateId = IndexedGuid.Generate();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedAt = DateTime.Now;
        }

        public Guid AggregateId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }
        public DateTime CreatedAt { get; }
    }
}
