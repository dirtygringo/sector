using System;
using NM.SharedKernel.Core.Bindings;
using NM.SharedKernel.Core.Storage.Standard;

namespace NM.Storage.MongoDb.Infrastructure.Factory
{
    internal class StandardStorageFactory<TEntity> : IStandardStorageFactory<TEntity> where TEntity : class, IEntity
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

        public IStandardStorage<TEntity> Storage => (IStandardStorage<TEntity>)_serviceProvider.GetService(typeof(IStandardStorage<TEntity>));

        #endregion
    }
}
