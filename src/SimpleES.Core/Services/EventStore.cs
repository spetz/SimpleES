using System;
using System.Collections.Generic;
using System.Linq;
using SimpleES.Core.Domain;
using SimpleES.Core.Events;

namespace SimpleES.Core.Services
{
    public class EventStore : IEventStore
    {
        private readonly List<EventInfo> _events = new List<EventInfo>();

        public EventStore()
        {
        }

        public T Load<T>(Guid aggregateId, int? version = null) where T : AggregateRoot, new()
        {
            version = version ?? int.MaxValue;
            var events = _events
                .Where(e => e.AggregateId == aggregateId && e.Version <= version)
                .OrderBy(x => x.Version)
                .Select(e => e.Data);
            if (!events.Any())
            {
                return null;
            }
            var aggregate = new T();
            aggregate.Replay(events);

            return aggregate;
        }

        public void Store<T>(T aggregate) where T : AggregateRoot
            => _events.AddRange(aggregate.Events
                .Select(e => new EventInfo
                    {
                        Id = e.Id,
                        AggregateId = aggregate.Id,
                        Timestamp = DateTime.UtcNow.Ticks,
                        Version = aggregate.Version,
                        Name = e.GetType().Name,
                        Data = e
                    }));

        private class EventInfo
        {
            public Guid Id { get; set; }
            public Guid AggregateId { get; set; }
            public long Timestamp { get; set; }
            public int Version { get; set; }
            public string Name { get; set; }
            public IEvent Data { get; set; }
        }        
    }
}