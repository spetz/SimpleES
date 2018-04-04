using System;

namespace SimpleES.Core.Events
{
    public class BalanceChanged : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public decimal Balance { get; }

        public BalanceChanged(decimal balance)
        {
            Balance = balance;
        }
    }
}