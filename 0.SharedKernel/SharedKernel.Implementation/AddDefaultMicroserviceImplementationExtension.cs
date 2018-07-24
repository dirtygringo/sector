using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Implementation.Domain;
using NM.SharedKernel.Implementation.StorageFactory;
using NM.SharedKernel.Infrastructure.Domain;
using NM.SharedKernel.Infrastructure.EventSourcing;
using NM.SharedKernel.Infrastructure.Query;

namespace NM.SharedKernel.Implementation
{
    public static class AddDefaultMicroserviceImplementationExtension
    {
        public static IServiceCollection AddDefaultMicroserviceImplementation(this IServiceCollection services)
        {
            services
                .AddSingleton(typeof(IEventStorageFactory<>), typeof(EventStorageFactory<>))
                .AddSingleton(typeof(IQueryStorageFactory<>), typeof(QueryStorageFactory<>))
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
