using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                .Include(x => x.Person).AsEnumerable();
        }
    }
}
