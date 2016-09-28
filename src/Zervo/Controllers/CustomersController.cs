using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zervo.Core.Models;
using Zervo.Core.Services.Contracts;
using Zervo.Models;

namespace Zervo.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(IMapper mapper, ICustomerService customerService)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.List();
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
        public void Post([FromBody]CustomerModel customer)
        {
            var customerModel = _mapper.Map<CustomerModel, CustomerObjectModel>(customer);
            _customerService.Create(customerModel);
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
