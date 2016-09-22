using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;
using Zervo.Data.Repositories.Database;

namespace Zervo.Data.Repositories
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
