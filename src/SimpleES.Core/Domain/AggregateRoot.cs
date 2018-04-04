using System;
using System.Collections.Generic;
using SimpleES.Core.Events;

namespace SimpleES.Core.Domain
{
    public abstract class AggregateRoot
    {
        private readonly ISet<IEvent> _events = new HashSet<IEvent>();
        private readonly Dictionary<Type, Action<IEvent>> _eventHandlers = new Dictionary<Type, Action<IEvent>>();
        public IEnumerable<IEvent> Events => _events;
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        public void Replay(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event, @new: false);
            }
        }

        protected void ApplyChange<T>(T @event, bool @new = true) where T : IEvent
        {
            _eventHandlers[@event.GetType()](@event);
            Version++;
            if (@new)
            {
                _events.Add(@event);
            }
        }

        protected void Handles<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            _eventHandlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        public void ClearEvents()
            => _events.Clear();
    }
}