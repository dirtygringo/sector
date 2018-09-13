using System;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NM.ServiceBus.Abstraction;

namespace NM.ServiceBus.RabbitMq
{
    public static class RabbitMqModule
    {
        internal static RabbitMqListener Listener { get; set; }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMq = configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>();
            services
                .AddTransient<IBus>(serviceProvider =>
                    RabbitHutch.CreateBus(
                        rabbitMq.Hostname,
                        rabbitMq.Port,
                        rabbitMq.VirtualHost,
                        rabbitMq.Username,
                        rabbitMq.Password,
                        rabbitMq.RequestedHeartbeat,
                    register => { }))
                .AddTransient<IBusClient, RabbitMqBus>()
                .AddTransient<IBusConfiguration, RabbitMqConfiguration>()
                .AddSingleton<RabbitMqListener>();

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
