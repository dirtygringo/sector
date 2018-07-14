using System;

namespace NM.SharedKernel.Infrastructure.EventSourcing
{
    public class EventData
    {
        #region Constructor

        protected EventData() { }

        public EventData(Guid aggregateId, string events, DateTime created)
        {
            AggregateId = aggregateId;
            Events = events;
            Created = created;
        }

        #endregion

        #region Properties

        public Guid AggregateId { get; protected set; }
        public string Events { get; protected set; }
        public DateTime Created { get; protected set; }

        #endregion

        #region Methods

        internal void SetEvents(string events)
        {
            Events = events;
        }

        #endregion
    }
}
