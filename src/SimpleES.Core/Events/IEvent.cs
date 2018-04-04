using System;

namespace SimpleES.Core.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}