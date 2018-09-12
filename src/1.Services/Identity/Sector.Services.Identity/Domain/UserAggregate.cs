using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using NM.Sector.Services.Identity.Contract.Events;
using NM.SharedKernel.Core.Domain;
using NM.SharedKernel.Core.Guards;
using NM.SharedKernel.Core.Messages;

namespace NM.Sector.Services.Identity.Domain
{
    internal sealed class UserAggregate : AggregateRoot
    {
        #region Constructor

        private UserAggregate(Guid aggregateId) : base(aggregateId)
        {
            RegisterHandlers<UserCreated>(Apply);
        }

        private UserAggregate(Guid aggregateId, IEnumerable<IDomainEvent> history) : this(aggregateId)
        {
            LoadHistory(history);
        }

        private UserAggregate(Guid aggregateId, string firstName, string lastName, string email, string passwordHash,
            string passwordSalt) : this(aggregateId)
        {
            ApplyChange(new UserCreated(aggregateId, firstName, lastName, email, passwordHash, passwordSalt, DateTime.Now));
        }

        #endregion

        #region Properties

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        #endregion

        #region FactoryMethods

        public static UserAggregate Create(Guid aggregateId, string firstName, string lastName, string email,
            string password)
        {
            Preconditions.CheckNullEmptyWhitespace(password, nameof(password));

            CreatePasswordHash(password, out string passwordHash, out string passwordSalt);
            return new UserAggregate(aggregateId, firstName, lastName, email, passwordHash, passwordSalt);
        }

        #endregion

        #region HelperMethods

        private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            Preconditions.CheckNullEmptyWhitespace(password, nameof(password));

            using (var hmac = new HMACSHA256())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        #endregion

        #region ApplyEvents

        private void Apply(UserCreated args)
        {
            FirstName = args.FirstName;
            LastName = args.LastName;
            Email = args.Email;
            PasswordHash = args.PasswordHash;
            PasswordSalt = args.PasswordSalt;
            CreatedAt = args.CreatedAt;
        }

        #endregion
    }
}
