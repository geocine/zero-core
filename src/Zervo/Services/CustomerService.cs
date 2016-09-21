using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Models;
using Zervo.Repositories;
using Zervo.Repositories.Database;
using Zervo.Services.Contracts;

namespace Zervo.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(ZervoContext context)
        {
            _customerRepository = new CustomerRepository(context);
        }

        public IEnumerable<Customer> List()
        {
            return _customerRepository.GetAll();
        }

        public void Create(Customer model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
