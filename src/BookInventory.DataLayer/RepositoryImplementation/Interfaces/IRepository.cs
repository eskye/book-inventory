using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookInventory.DataLayer.RepositoryImplementation.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entity, bool saveNow = true); 
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task InsertAsync(TEntity entity);
        Task<TEntity> InsertAndGetEntityAsync(TEntity entity);
        Task<TEntity> GetAsync(long id);
        Task InsertRangeAsync(IEnumerable<TEntity> entities);  
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);

    }
}
