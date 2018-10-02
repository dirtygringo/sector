﻿using System;
using NM.SharedKernel.Core.Query;
using NM.SharedKernel.Core.Storage.Query;

namespace NM.Storage.MongoDb.Infrastructure.Factory
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
