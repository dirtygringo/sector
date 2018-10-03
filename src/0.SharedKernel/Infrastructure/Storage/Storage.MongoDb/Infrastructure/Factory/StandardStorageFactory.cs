using System;
using NM.SharedKernel.Core.Query;
using NM.SharedKernel.Core.Storage.Standard;

namespace NM.Storage.MongoDb.Infrastructure.Factory
{
    internal class StandardStorageFactory<TQueryEntity> : IStandardStorageFactory<TQueryEntity> where TQueryEntity : class, IQueryEntity
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        public StandardStorageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Properties

        public IStandardStorage<TQueryEntity> Storage => (IStandardStorage<TQueryEntity>)_serviceProvider.GetService(typeof(IStandardStorage<TQueryEntity>));

        #endregion
    }
}
