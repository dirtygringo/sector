using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace NM.SharedKernel.Implementation.Storages.Mongo
{
    internal class MongoDatabaseInitializer : IDatabaseInitializer
    {
        #region Fields

        private bool _initialized;
        private readonly bool _seed;
        private readonly IDatabaseSeeder _seeder;

        #endregion

        #region Constructor

        public MongoDatabaseInitializer(IDatabaseSeeder seeder, IOptions<MongoOptions> options)
        {
            _seeder = seeder;
            _seed = options.Value.Seed;
        }

        #endregion

        #region Methods

        public async Task InitializeAsync()
        {
            if (_initialized) return;
            RegisterConventions();

            _initialized = true;

            if(!_seed) return;
            await _seeder.SeedAsync();
        }

        private static void RegisterConventions()
        {
            ConventionRegistry.Register("SecotrConventions", new MongoConvention(), x => true);
        }

        #endregion

        #region NestedTypes

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }

        #endregion
    }
}
