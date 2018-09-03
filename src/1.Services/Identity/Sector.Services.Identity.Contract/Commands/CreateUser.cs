﻿using System;
using NM.SharedKernel.Infrastructure.Helpers;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.Sector.Services.Identity.Contract.Commands
{
    public class CreateUser : ICommand
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