using System;
using System.Collections.Generic;
using Zervo.Core.Services.Contracts;
using Zervo.Data.Repositories;
using Zervo.Data.Repositories.Database;
using System.Linq;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;
using Customer = Zervo.Core.Models.Customer;

namespace Zervo.Core.Services
{
    public class CustomerService : Service<Customer> , ICustomerService
    {
        private readonly IRepository<Data.Models.Customer> _repository;

        public CustomerService(IRepository<Data.Models.Customer> repository )
        {
            _repository = repository;
        }

        public override void Create(Customer customer)
        {
            var customerModel = new Data.Models.Customer()
            {
                Person = new Person()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email
                }
            };
            _repository.Add(customerModel);
            _repository.SaveChanges();
        }

        public override IEnumerable<Customer> List()
        {
            // Need to use automapper here
            var customers = _repository.GetAllDetails();
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
