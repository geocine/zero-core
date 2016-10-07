using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var changes = base.SaveChanges();
            return changes;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(CancellationToken.None);
        }

        [SuppressMessage("ReSharper", "OptionalParameterHierarchyMismatch")]
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
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
