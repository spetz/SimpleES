using System;
using SimpleES.Core.Domain;

namespace SimpleES.Core.Services
{
    public interface ICustomerService
    {
        Customer Get(Guid id);
        Customer Load(Guid id, int? version = null);
        void Create(Guid id, string email);
        void ChangeBalance(Guid id, decimal balance);        
    }
}