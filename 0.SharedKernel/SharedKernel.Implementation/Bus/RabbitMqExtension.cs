using System;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NM.SharedKernel.Implementation.Processes;
using NM.SharedKernel.Infrastructure.Bus;
using NM.SharedKernel.Infrastructure.Processes;

namespace NM.SharedKernel.Implementation.Bus
{
    public static class RabbitMqExtension
    {
        internal static RabbitMqListener Listener { get; set; }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMq = configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>();
            services
                .AddSingleton<IPublisher, Publisher>()
                .AddSingleton<ISender, Sender>()
                .AddTransient<IBus>(_ => RabbitHutch.CreateBus(rabbitMq.Hostname,
                    rabbitMq.Port,
                    rabbitMq.VirtualHost,
                    rabbitMq.Username,
                    rabbitMq.Password,
                    rabbitMq.RequestedHeartbeat,
                    register => { }))
                .AddTransient<IBusClient, RabbitMqBus>()
                .AddSingleton<IBusConfiguration, RabbitMqConfiguration>();

            return services;
        }

        public static IApplicationBuilder UseRabbitMq(this IApplicationBuilder app, Action<IBusConfiguration> config)
        {
            Listener = (RabbitMqListener)app.ApplicationServices.GetService(typeof(RabbitMqListener));
            var lifetime = (IApplicationLifetime)app.ApplicationServices.GetService(typeof(IApplicationLifetime));

            lifetime.ApplicationStarted.Register(() => Listener.Register(config));
            lifetime.ApplicationStopped.Register(Listener.Unregister);

            return app;
        }
    }
}
