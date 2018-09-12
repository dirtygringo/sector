using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Core.Domain;

namespace SharedKernel.Core.Domain
{
    public static class CoreDomainModule
    {
        public static IServiceCollection AddCoreDomain(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
