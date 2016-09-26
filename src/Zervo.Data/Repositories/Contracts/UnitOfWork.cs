using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zervo.Data.Exceptions;

namespace Zervo.Data.Repositories.Contracts
{
    public class UnitOfWork: IUnitOfWork
    {
        #region Private Fields

        private IDataContext _dataContext;
        private readonly IServiceProvider _serviceProvider;
        private bool _isDisposed;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(IDataContext dataContext, IServiceProvider serviceProvider)
        {
            _dataContext = dataContext;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {

                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }

            _isDisposed = true;
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            CheckDisposed();
            return _dataContext.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            CheckDisposed();
            var repositoryType = typeof(IRepository<TEntity>);
            var repository = (IRepository<TEntity>)_serviceProvider.GetService(repositoryType);
            if (repository == null)
            {
                throw new RepositoryNotFoundException(repositoryType.Name, String.Format("Repository {0} not found in the IOC container. Check if it is registered during startup.", repositoryType.Name));
            }

            return repository;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        protected void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException("The UnitOfWork is already disposed and cannot be used anymore.");
        }
    }
}
