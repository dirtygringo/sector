using System;
using System.Collections.Generic;
using System.Linq;
using NM.SharedKernel.Infrastructure.EventSourcing;
using NM.SharedKernel.Infrastructure.Messages;

namespace NM.SharedKernel.Infrastructure.Domain
{
    public abstract class AggregateRoot : IEventSourced
    {
        #region Fields

        private readonly Dictionary<Type, Action<IDomainEvent>> _handlers;
        private readonly IList<IDomainEvent> _changes;

        #endregion

        #region Constructor

        protected AggregateRoot(Guid id)
        {
            Id = id;
            _handlers = new Dictionary<Type, Action<IDomainEvent>>();
            _changes = new List<IDomainEvent>();
        }

        #endregion

        #region Properties
        public Guid Id { get; private set; }
        public int Version { get; private set; } = -1;
        public IEnumerable<IDomainEvent> UncomittedChanges => _changes;

        #endregion

        #region Methods

        protected void RegisterHandlers<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent
        {
            if(_handlers.Keys.Contains(typeof(TEvent))) throw new ArgumentException("Cannot register same handler twice.");
            _handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        protected void LoadHistory(IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvent in events) Apply(domainEvent, false);
        }

        protected void ApplyChange<TEvent>(TEvent @event) where TEvent : IDomainEvent => Apply(@event, true);

        private void Apply<TEvent>(TEvent @event, bool isNew) where TEvent : IDomainEvent
        {
            Version++;
            _handlers[@event.GetType()].Invoke(@event);

            if(isNew) _changes.Add(@event);
        }

        #endregion
    }
}
