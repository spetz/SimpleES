using System;
using System.Collections.Generic;
using System.Linq;
using SimpleES.Core.Domain;

namespace SimpleES.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new List<Customer>();
        private readonly IEventStore _eventStore;

        public CustomerService(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public Customer Get(Guid id)
            => _customers.SingleOrDefault(x => x.Id == id);

        public Customer Load(Guid id, int? version = null)
            => _eventStore.Load<Customer>(id, version);

        public void Create(Guid id, string email)
        {
            var customer = new Customer(id, email);
            _customers.Add(customer);
            _eventStore.Store(customer);
            customer.ClearEvents();
        }

        public void ChangeBalance(Guid customerId, decimal balance)
        {
            var customer = Get(customerId);
            customer.ChangeBalance(balance);
            _eventStore.Store(customer);
            customer.ClearEvents();
        }
    }
}