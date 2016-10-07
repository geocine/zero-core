using System.Collections.Generic;
using System.Threading.Tasks;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Domain.Services.Contracts
{
    public interface IService<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> List();
        int Create(TEntity model);
        void Update(TEntity model);
        void Delete(int id);
    }
}
