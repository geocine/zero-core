using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zervo.Data.Helpers;

namespace Zervo.Data.Repositories.Contracts
{
    public class DataContext<T> : DbContext, IDataContext where T : DbContext
    {
        #region Private Fields
        private bool _disposed;
        #endregion Private Fields

        public DataContext(DbContextOptions<T> options) : base(options)
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; }

        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }

        [SuppressMessage("ReSharper", "OptionalParameterHierarchyMismatch")]
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SyncObjectsStatePreCommit();
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            SyncObjectsStatePostCommit();
            return changesAsync;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IEntity)dbEntityEntry.Entity).ObjectState);
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IEntity)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        public override void Dispose()
        {
            if (!_disposed)
            {
                // free other managed objects that implement
                // IDisposable only

                // release any unmanaged objects
                // set object references to null
                _disposed = true;
            }

            base.Dispose();
        }
    }
}
