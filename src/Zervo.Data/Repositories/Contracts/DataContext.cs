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
            //SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            //SyncObjectsStatePostCommit();
            return changes;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(CancellationToken.None);
        }

        [SuppressMessage("ReSharper", "OptionalParameterHierarchyMismatch")]
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            return changesAsync;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public override void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }

            base.Dispose();
        }
    }
}
