using System;
using System.Collections.Generic;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Database;
using System.Linq;
using Zervo.Core.Models;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Core.Services
{
    public class CustomerService : Service<Customer> , ICustomerService
    {
        private readonly IRepository<Data.Models.Customer> _customerRepository;

        public CustomerService(IRepository<Data.Models.Customer> customerRepository )
        {
            _customerRepository = customerRepository;
        }

        public override IEnumerable<Customer> List()
        {
            // Need to use automapper here
            var customers = _customerRepository.GetAllDetails();
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
