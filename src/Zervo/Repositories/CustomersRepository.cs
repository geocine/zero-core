using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Models;
using Zervo.Repositories.Database;

namespace Zervo.Repositories
{
    public class CustomersRepository
    {
        private readonly ZervoContext _dbContext;

        public CustomersRepository(ZervoContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }
    }
}
