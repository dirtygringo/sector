using System;
using NM.SharedKernel.Core.Abstraction.Query;

namespace NM.SharedKernel.Core.Factory
{
    internal class QueryStorageFactory<TQueryEntity> : IQueryStorageFactory<TQueryEntity> where TQueryEntity : class, IQueryEntity
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        public QueryStorageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Properties

        public IQueryStorage<TQueryEntity> Storage => (IQueryStorage<TQueryEntity>)_serviceProvider.GetService(typeof(IQueryStorage<TQueryEntity>));

        #endregion
    }
}
