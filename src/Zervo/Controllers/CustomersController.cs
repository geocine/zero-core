using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zervo.Core.Services.Contracts;

namespace Zervo.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.List().Select(x => x.Person).ToList();
            return Ok(customers);
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/customers
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
