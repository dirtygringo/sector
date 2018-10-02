using Microsoft.Extensions.Logging;
using NM.SharedKernel.Core.Abstraction.Domain;
using NM.SharedKernel.Core.Abstraction.Storage.Event;

namespace NM.Sector.Services.Identity.Domain
{
    internal class UserRepository : BaseRepository<UserAggregate>
    {
        public UserRepository(ILogger<BaseRepository<UserAggregate>> logger, IEventStorageFactory<UserAggregate> eventStorageFactory) : base(logger, eventStorageFactory) { }
    }
}
