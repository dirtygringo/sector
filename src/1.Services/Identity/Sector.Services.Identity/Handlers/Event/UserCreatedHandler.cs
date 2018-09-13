using System;
using System.Threading.Tasks;
using NM.Sector.Services.Identity.Contract.Events;
using NM.SharedKernel.Core.Abstraction.Workers;

namespace NM.Sector.Services.Identity.Handlers.Event
{
    internal sealed class UserCreatedHandler : IMessageHandler<UserCreated>
    {
        public Task HandleAsync(UserCreated message)
        {
            throw new NotImplementedException();
        }
    }
}
