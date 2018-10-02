using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NM.SharedKernel.Core.Abstraction.Storage.Event;
using NM.SharedKernel.Core.Abstraction.Storage.Query;
using NM.Storage.MongoDb.Abstraction;
using NM.Storage.MongoDb.Infrastructure;
using NM.Storage.MongoDb.Infrastructure.Factory;

namespace NM.Storage.MongoDb
{
    public static class MongoModule
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(options =>
            {
                var mongoOptions = configuration.GetSection(nameof(MongoOptions)).Get<MongoOptions>();
                options.ConnectionString = mongoOptions.ConnectionString;
                options.Database = mongoOptions.Database;
                options.Seed = mongoOptions.Seed;
            });

            services.AddSingleton<MongoClient>(c =>
            {
                var mongoOptions = c.GetService<IOptions<MongoOptions>>();
                var client = new MongoClient(mongoOptions.Value.ConnectionString);
                return client;
            });
            services.AddTransient<IMongoDatabase>(c =>
            {
                var mongoOptions = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(mongoOptions.Value.Database);
            });
            services
                .AddTransient<IDatabaseInitializer, MongoDatabaseInitializer>()
                .AddTransient<IDatabaseSeeder,MongoDatabaseSeeder>()
                .AddSingleton(typeof(IEventStorageFactory<>), typeof(EventStorageFactory<>))
                .AddSingleton(typeof(IQueryStorageFactory<>), typeof(QueryStorageFactory<>))
                .AddTransient(typeof(IEventStorage<>), typeof(MongoEventStorage<>));

            return services;
        }

        public static IApplicationBuilder UseMongoDb(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            return app;
        }
    }
}
