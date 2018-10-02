using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NM.SharedKernel.Core.EventSourcing;
using NM.SharedKernel.Core.Messages;

namespace NM.Storage.MongoDb.Format
{
    internal static class EventDataExtensions
    {
        #region Fields

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None, Formatting = Formatting.None };

        #endregion

        #region ExtensionMethods

        internal static EventData ToEventData<TEventSourced>(this TEventSourced source) where TEventSourced : class, IEventSourced
        {
            var list = new List<EventMetadata>();
            var version = source.Version;

            foreach (var e in source.UncomittedChanges)
            {
                list.Add(
                    new EventMetadata()
                    {
                        Type = e.GetType(),
                        TypeName = e.GetType().Name,
                        EventHeaders = new Dictionary<string, object> { { "EventClrType", e.GetType().FullName } },
                        Data = JsonConvert.SerializeObject(e, _serializerSettings),
                        Version = version
                    });

                version++;
            }

            return new EventData(source.Id, JsonConvert.SerializeObject(list, _serializerSettings), DateTime.Now);
        }

        internal static IEnumerable<IDomainEvent> DeserializeEvents(this EventData eventData)
        {
            foreach (var meta in Deserialize(eventData.Events))
            {
                yield return JsonConvert.DeserializeObject(meta.Data, meta.Type) as IDomainEvent;
            }
        }

        internal static void AppendEvents<TEventSourced>(this EventData eventData, TEventSourced source) where TEventSourced : class, IEventSourced
        {
            var currentEventList = Deserialize(eventData.Events);
            var version = source.Version;

            foreach (var e in source.UncomittedChanges)
            {
                ++version;
                currentEventList.Add(
                    new EventMetadata()
                    {
                        Type = e.GetType(),
                        TypeName = e.GetType().Name,
                        EventHeaders = new Dictionary<string, object> { { "EventClrType", e.GetType().FullName } },
                        Data = JsonConvert.SerializeObject(e, _serializerSettings),
                        Version = version
                    });
            }
            eventData.Events = JsonConvert.SerializeObject(currentEventList, _serializerSettings);
        }

        private static List<EventMetadata> Deserialize(string events)
        {
            return JsonConvert.DeserializeObject<List<EventMetadata>>(events, _serializerSettings);
        }

        #endregion

        #region NestedTypes

        private class EventMetadata
        {
            public Type Type { get; set; }
            public string TypeName { get; set; }
            public Dictionary<string, object> EventHeaders { get; set; }
            public int Version { get; set; }
            public string Data { get; set; }
        }

        #endregion
    }
}
