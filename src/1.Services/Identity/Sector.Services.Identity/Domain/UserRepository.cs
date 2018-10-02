using Microsoft.Extensions.Logging;
using NM.SharedKernel.Core.Domain;
using NM.SharedKernel.Core.Storage.Event;

namespace NM.Sector.Services.Identity.Domain
{
    internal class UserRepository : BaseRepository<UserAggregate>
    {
        public UserRepository(ILogger<BaseRepository<UserAggregate>> logger, IEventStorageFactory<UserAggregate> eventStorageFactory) : base(logger, eventStorageFactory) { }
    }
}
