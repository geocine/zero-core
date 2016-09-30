using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zervo.Data.Repositories.Contracts
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
