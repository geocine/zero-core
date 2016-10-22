using System.Threading.Tasks;
using Zervo.Data.Models;

namespace Zervo.Domain.Services.Contracts
{
    public interface IUserService : IService<User>
    {
        User FindByUserName(string userName);
    }
}
