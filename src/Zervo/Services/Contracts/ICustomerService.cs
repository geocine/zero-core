using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Data.Models;

namespace Zervo.Services.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<Customer> List();
        void Create(Customer model);
        void Delete(int id);
    }
}
