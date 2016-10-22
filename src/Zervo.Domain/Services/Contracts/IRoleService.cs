using System.Threading.Tasks;
using Zervo.Data.Models;

namespace Zervo.Domain.Services.Contracts
{
    public interface IRoleService : IService<Role>
    {
        Role FindByName(string userName);
    }
}
