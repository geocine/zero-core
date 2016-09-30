using System;
using System.Collections.Generic;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Domain.Services.Contracts
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {
        public virtual void Create(TEntity model)
        {
            
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> List()
        {
            throw new NotImplementedException();
        }
    }
}
