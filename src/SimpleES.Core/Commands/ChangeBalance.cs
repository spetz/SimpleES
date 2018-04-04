using System;
using Newtonsoft.Json;

namespace SimpleES.Core.Commands
{
    public class ChangeBalance : ICommand
    {
        public Guid CustomerId { get; }
        public decimal Balance { get; }

        [JsonConstructor]
        public ChangeBalance(Guid customerId, decimal balance)
        {
            CustomerId = customerId;
            Balance = balance;
        }        
    }
}