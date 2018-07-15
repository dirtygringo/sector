using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NM.Sector.Services.Identity.Contract.Commands;
using NM.Sector.Services.Identity.Domain;
using NM.SharedKernel.Infrastructure.Domain;
using NM.SharedKernel.Infrastructure.Processes;

namespace NM.Sector.Services.Identity.Handlers.Command
{
    internal sealed class CreateUserHandler : IMessageHandler<CreateUser>
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IRepository<UserAggregate> _repository;

        #endregion

        #region Constructor

        public CreateUserHandler(ILogger logger, IRepository<UserAggregate> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #endregion

        #region Methods

        public Task Handle(CreateUser args)
        {
            try
            {
                var aggregate = UserAggregate.Create(args.AggregateId, args.FirstName, args.LastName, args.Email, args.Password);
                return _repository.SaveAsync(aggregate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    _logger.LogError(ex.Message, ex);
                }
                throw;
            }
        }

        #endregion
    }
}
