using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zervo.Data.Infrastructure;
using Zervo.Data.Repositories.Contracts;
using Zervo.Data.Repositories.Database;

namespace Zervo.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {

        private readonly IDataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        #region Properties
        public Repository(IDataContext context)
        {
            _context = context;

            var dbContext = context as DbContext;

            if (dbContext != null)
            {
                _dbSet = dbContext.Set<TEntity>();
            }
            //else
            //{
            //    var fakeContext = context as FakeDbContext;
            //
            //    if (fakeContext != null)
            //    {
            //        _dbSet = fakeContext.Set<TEntity>();
            //    }
            //}
        }
        #endregion

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }
        public virtual IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public TEntity GetSingle(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = new TEntity() { Id = id };
            Delete(entity);
        }
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = _dbSet.Where(predicate);

            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
