using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;
using Zervo.Data.Repositories.Database;

namespace Zervo.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, ICustomerRepository
    {
        private readonly ZervoContext _context;

        public EmployeeRepository(ZervoContext context)
            : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().Include(x => x.Person).AsEnumerable();
        }

    }
}
