using System.Collections.Generic;
using Zervo.Core.Models.Contracts;
using Zervo.Data.Infrastructure;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Core.Services.Contracts
{
    public interface IService<TEntity> where TEntity : IObjectModel
    {
        IEnumerable<TEntity> List();
        void Create(TEntity model);
        void Delete(int id);
    }
}
