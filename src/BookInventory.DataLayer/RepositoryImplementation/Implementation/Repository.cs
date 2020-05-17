using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookInventory.DataLayer.RepositoryImplementation.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.DataLayer.RepositoryImplementation.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;

        private readonly IUnitOfWork _unitOfWork;
        protected readonly BookInventoryDbContext Context;

        public Repository(BookInventoryDbContext context, IUnitOfWork unitOfWork)
        {
            Context = context;
            _unitOfWork = unitOfWork;
            DbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Table => DbSet;
        public async Task DeleteAsync(TEntity entity, bool saveNow = true)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            if (saveNow)
                await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> InsertAndGetEntityAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> GetAsync(long id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }


        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

        }

        public void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Context.Set<TEntity>().UpdateRange(entities);
            if (!saveNow)
                return;
            _unitOfWork.SaveChanges();
        }

    }
}
