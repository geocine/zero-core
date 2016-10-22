using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AspNet.Security.OAuth.Validation;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zervo.Data.Models;
using Zervo.Domain.Services.Contracts;
using Zervo.Features.Customers;
using Zervo.Mappings;
using Zervo.ViewModels;

namespace Zervo.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/customers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _mediator.SendAsync(new GetCustomerListQuery());
            return Ok(customers);
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _mediator.SendAsync(new GetCustomerQuery { Id = id });
            return Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerViewModel customer)
        {
            customer.Id = await _mediator.SendAsync(new AddCustomerCommand { CustomerViewModel = customer });
            return CreatedAtRoute("GetCustomer", new { customer.Id }, customer);
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromUri] int id, [FromBody]CustomerViewModel customer)
        {
            await _mediator.SendAsync(new UpdateCustomerCommand { Id = id, CustomerViewModel = customer });
            return NoContent();
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.SendAsync(new DeleteCustomerCommand { Id = id });
            return Ok();
        }
    }
}
