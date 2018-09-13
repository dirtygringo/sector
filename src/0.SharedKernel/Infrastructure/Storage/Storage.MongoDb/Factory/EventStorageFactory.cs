using System;
using NM.SharedKernel.Core.Abstraction.EventSourcing;
using NM.Storage.Abstraction.Event;

namespace NM.Storage.MongoDb.Factory
{
    internal class EventStorageFactory<TEventSourced> : IEventStorageFactory<TEventSourced> where TEventSourced : class, IEventSourced
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        public EventStorageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Properties

        public IEventStorage<TEventSourced> Storage => (IEventStorage<TEventSourced>) _serviceProvider.GetService(typeof(IEventStorage<TEventSourced>));

        #endregion
    }
}
