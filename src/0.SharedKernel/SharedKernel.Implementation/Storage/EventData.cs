using System;

namespace NM.SharedKernel.Implementation.Storage
{ 
    internal class EventData
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

        public Guid AggregateId { get; set; }
        public string Events { get; set; }
        public DateTime Created { get; set; }

        #endregion
    }
}
