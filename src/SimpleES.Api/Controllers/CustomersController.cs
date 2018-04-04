using System;
using SimpleES.Core.Commands;
using SimpleES.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace SimpleES.Api.Controllers
{
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var customer = _customerService.Get(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(new {customer.Id, customer.Email, 
                customer.Balance, customer.Version});
        }
    
        [HttpGet("{id}/load")]
        public IActionResult Load(Guid id, int? version = null)
        {
            var customer = _customerService.Load(id, version);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(new {customer.Id, customer.Email, 
                customer.Balance, customer.Version});
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomer command)
        {
            var id = Guid.NewGuid();
            _customerService.Create(id, command.Email);

            return Created($"customers/{id}", null);
        }

        [HttpPost("balance")]
        public IActionResult Post([FromBody] ChangeBalance command)
        {
            _customerService.ChangeBalance(command.CustomerId, command.Balance);

            return Ok();
        }
    }
}