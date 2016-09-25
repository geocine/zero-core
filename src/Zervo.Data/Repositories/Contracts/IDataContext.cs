using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zervo.Data.Repositories.Contracts
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void SyncObjectsStatePostCommit();
    }
}
