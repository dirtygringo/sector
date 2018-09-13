using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Core.Abstraction.Domain;
using NM.SharedKernel.Core.Abstraction.EventSourcing;
using NM.SharedKernel.Core.Abstraction.Processes;
using NM.SharedKernel.Core.Abstraction.Query;
using NM.SharedKernel.Core.Factory;
using NM.SharedKernel.Core.Processes;

namespace NM.SharedKernel.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
                .AddSingleton<IPublisher, Publisher>()
                .AddSingleton<ISender, Sender>()
                .AddSingleton(typeof(IEventStorageFactory<>), typeof(EventStorageFactory<>))
                .AddSingleton(typeof(IQueryStorageFactory<>), typeof(QueryStorageFactory<>))
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
