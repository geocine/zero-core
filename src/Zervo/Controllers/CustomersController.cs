using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zervo.Data.Models;
using Zervo.Domain.Services.Contracts;
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
            var customerList = customers.Select(x => _mapper.Map<Customer, CustomerViewModel>(x)).ToList();
            return Ok(customerList);
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _customerService.Get(id);
            return Ok(_mapper.Map<Customer,CustomerViewModel>(customer));
        }

        // POST api/customers
        [HttpPost]
        public IActionResult Post([FromBody]CustomerModel customer)
        {
            var customerModel = _mapper.Map<CustomerModel, Customer>(customer);
            _customerService.Create(customerModel);
            var customerViewModel = _mapper.Map<Customer, CustomerViewModel>(customerModel);
            return CreatedAtRoute("GetCustomer", new { id = customerModel.Id }, customerViewModel);
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
