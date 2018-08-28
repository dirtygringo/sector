using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Implementation.Domain;
using NM.SharedKernel.Implementation.Storages;
using NM.SharedKernel.Infrastructure.Domain;
using NM.SharedKernel.Infrastructure.EventSourcing;
using NM.SharedKernel.Infrastructure.Query;

namespace NM.SharedKernel.Implementation
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddSingleton(typeof(IEventStorageFactory<>), typeof(EventStorageFactory<>))
                .AddSingleton(typeof(IQueryStorageFactory<>), typeof(QueryStorageFactory<>))
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
