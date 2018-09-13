using System;
using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Core.Abstraction.Domain;
using NM.SharedKernel.Core.Abstraction.Workers;
using NM.SharedKernel.Core.Domain;
using NM.SharedKernel.Core.Workers;

namespace NM.SharedKernel.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services, Action<IServiceCollection> registerDependency)
        {
            services
                .AddSingleton<IPublisher, Publisher>()
                .AddSingleton<ISender, Sender>()
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));

            registerDependency?.Invoke(services);

            return services;
        }
    }
}
