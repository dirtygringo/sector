using System;
using System.Collections.Generic;
using System.Linq;
using NM.SharedKernel.Core.EventSourcing;
using NM.SharedKernel.Core.Guards;
using NM.SharedKernel.Core.Messages;

namespace NM.SharedKernel.Core.Domain
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
            Preconditions.CheckNullOrEmpty(id, nameof(id));

            Id = id;
            _handlers = new Dictionary<Type, Action<IDomainEvent>>();
            _changes = new List<IDomainEvent>();
        }

        #endregion

        #region Properties
        public Guid Id { get; }
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
