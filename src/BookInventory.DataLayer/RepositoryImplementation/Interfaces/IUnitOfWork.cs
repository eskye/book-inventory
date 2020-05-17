using System.Data;
using System.Threading;
using System.Threading.Tasks; 

namespace BookInventory.DataLayer.RepositoryImplementation.Interfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Rollback();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void Commit();
        void Dispose();
    }
}
