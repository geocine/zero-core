using System.Collections.Generic;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Domain.Services.Contracts
{
    public interface IService<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> List();
        void Create(TEntity model);
        void Delete(int id);
    }
}
