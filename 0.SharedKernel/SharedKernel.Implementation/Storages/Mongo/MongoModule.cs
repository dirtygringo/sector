using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace NM.SharedKernel.Implementation.Storages.Mongo
{
    public static class MongoModule
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoOptions = configuration.GetSection(nameof(MongoOptions)).Get<MongoOptions>();
            services.AddSingleton<MongoClient>(c => new MongoClient(mongoOptions.ConnectionString));
            services.AddScoped<IMongoDatabase>(c =>
            {
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(mongoOptions.Database);
            });
            services.AddScoped<IDatabaseInitializer, MongoDatabaseInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoDatabaseSeeder>();

            return services;
        }
    }
}
