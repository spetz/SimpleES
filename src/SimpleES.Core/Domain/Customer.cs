using System;
using SimpleES.Core.Events;

namespace SimpleES.Core.Domain
{
    public class Customer : AggregateRoot
    {
        public string Email { get; private set; }
        public decimal Balance { get; private set; }

        public Customer()
        {
            Handles<CustomerCreated>(Apply);
            Handles<BalanceChanged>(Apply);
        }

        public Customer(Guid id, string email) : this()
        {
            ApplyChange(new CustomerCreated(id, email));
        }

        public void ChangeBalance(decimal balance)
        {
            if (Balance + balance < 0)
            {
                throw new Exception("Invalid balance.");
            }
            ApplyChange(new BalanceChanged(balance));
        }

        private void Apply(CustomerCreated @event)
        {
            Id = @event.CustomerId;
            Email = @event.Email;
        }

        private void Apply(BalanceChanged @event)
        {
            Balance += @event.Balance;
        }
    }
}