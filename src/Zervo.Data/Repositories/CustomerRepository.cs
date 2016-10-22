using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Repositories
{
    public static class CustomerRepository
    {
        public static IEnumerable<Customer> GetAllDetails(this IRepository<Customer> repository)
        {
            return repository
                .Queryable()
                .Include(x => x.User).AsEnumerable();
        }

        public static Customer GetCustomerDetails(this IRepository<Customer> repository, int id)
        {
            return repository.Queryable().Include(x => x.User).SingleOrDefault(x => x.Id == id);
        }
    }
}
