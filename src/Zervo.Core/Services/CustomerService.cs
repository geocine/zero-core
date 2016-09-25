using System;
using System.Collections.Generic;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Database;
using System.Linq;
using Zervo.Core.Models;

namespace Zervo.Core.Services
{
    public class CustomerService : Service<Customer> , ICustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(ZervoContext context)
        {
            _customerRepository = new CustomerRepository(context);
        }

        public override IEnumerable<Customer> List()
        {
            // Need to use automapper here
            var customers = _customerRepository.GetAll();
            // Also add the employee id x.Id
            var customerList = customers.Select(x => new Customer
            {
                Id = x.Id,
                FirstName = x.Person.FirstName,
                LastName = x.Person.LastName,
                Email = x.Person.Email
            }).ToList();
            return customerList;
        }

    }
}
