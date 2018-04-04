using System;
using Newtonsoft.Json;

namespace SimpleES.Core.Commands
{
    public class CreateCustomer : ICommand
    {
        public Guid Id => Guid.NewGuid();
        public string Email { get; }

        [JsonConstructor]
        public CreateCustomer(string email)
        {
            Email = email;
        }
    }
}