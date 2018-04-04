using System;
using SimpleES.Core.Domain;

namespace SimpleES.Core.Services
{
    public interface IEventStore
    {
        T Load<T>(Guid aggregateId, int? version = null) where T : AggregateRoot, new();
        void Store<T>(T aggregate) where T : AggregateRoot;        
    }
}