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
        private readonly CustomersRepository _customersRepository;

        public CustomerService(ZervoContext context)
        {
            _customersRepository = new CustomersRepository(context);
        }

        public IEnumerable<Customer> List()
        {
            return _customersRepository.GetAll();
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
