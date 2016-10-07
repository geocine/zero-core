using System.Threading.Tasks;
using Zervo.Data.Models;

namespace Zervo.Domain.Services.Contracts
{
    public interface ICustomerService : IService<Customer>
    {
        Customer Get(int id);
    }
}
