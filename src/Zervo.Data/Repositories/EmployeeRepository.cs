using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Repositories
{
    public static class EmployeeRepository
    {
        public static IEnumerable<Employee> GetAllDetails(this IRepository<Employee> repository)
        {
            return repository
                .Queryable()
                .Include(x => x.Person).AsEnumerable();
        }
    }
}
