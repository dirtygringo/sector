using System;
using System.Threading.Tasks;
using NM.Sector.Services.Identity.Contract.Events;
using NM.SharedKernel.Infrastructure.Processes;

namespace NM.Sector.Services.Identity.Handlers.Event
{
    internal sealed class UserCreatedHandler : IMessageHandler<UserCreated>
    {
        public Task Handle(UserCreated message)
        {
            throw new NotImplementedException();
        }
    }
}
