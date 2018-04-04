using System;

namespace SimpleES.Core.Events
{
    public class CustomerCreated : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid CustomerId { get; }
        public string Email { get; }
        
        public CustomerCreated(Guid customerId, string email)
        {
            CustomerId = customerId;
            Email = email;
        }
    }
}