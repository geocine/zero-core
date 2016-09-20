using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Models;

namespace Zervo.Services.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<Customer> List();
        void Create(Customer model);
        void Delete(int id);
    }
}
