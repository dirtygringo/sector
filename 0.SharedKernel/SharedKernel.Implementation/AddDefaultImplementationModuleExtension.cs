﻿using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Implementation.Domain;
using NM.SharedKernel.Implementation.Processes;
using NM.SharedKernel.Implementation.StorageFactory;
using NM.SharedKernel.Infrastructure.Domain;
using NM.SharedKernel.Infrastructure.EventSourcing;
using NM.SharedKernel.Infrastructure.Processes;
using NM.SharedKernel.Infrastructure.Query;

namespace NM.SharedKernel.Implementation
{
    public static class AddDefaultImplementationModuleExtension
    {
        public static IServiceCollection AddDefaultMicroserviceImplementation(this IServiceCollection services)
        {
            services
                .AddSingleton<IPublisher, Publisher>()
                .AddSingleton<ISender, Sender>()
                .AddSingleton(typeof(IEventStorageFactory<>), typeof(EventStorageFactory<>))
                .AddSingleton(typeof(IQueryStorageFactory<>), typeof(QueryStorageFactory<>))
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));

        }
    }
}
