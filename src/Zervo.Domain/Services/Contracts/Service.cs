using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Domain.Services.Contracts
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _repository;

        protected Service( IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual int Create(TEntity model)
        {
            _repository.Add(model);
            _repository.SaveChanges();
            return model.Id;
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
            _repository.SaveChanges();
        }


        public virtual void Update(TEntity model)
        {
            _repository.Update(model);
            _repository.SaveChanges();
        }

        public virtual IEnumerable<TEntity> List()
        {
            return _repository.GetAll();
        }
    }
}
