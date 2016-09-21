using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zervo.Models;
using Zervo.Repositories.Contracts;
using Zervo.Repositories.Database;

namespace Zervo.Repositories
{
    public class CustomerRepository : EntityBaseRepository<Customer> , ICustomerRepository
    {
        private readonly ZervoContext _context;

        public CustomerRepository(ZervoContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
