

using E_Commerce.Data.Context;
using E_Commerce.Data.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly E_commenceContext _context;
        protected DbSet<TEntity> Entity { get; }

        public BaseRepository(E_commenceContext context)
        {
            _context = context;
            Entity = context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAllListAsync()
        {
            return await Entity.ToListAsync();
        }

        public IQueryable<TEntity> GetAllQuery()
        {
            return Entity.AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllQueryWithInclude(List<string> properties)
        {
            var query = Entity.AsQueryable();

            foreach (var property in properties)
            {
                query.Include(property);
            }

            return query;
        }

        public virtual async Task<List<TEntity>> GetAllListWithInclude(List<string> properties)
        {
            var query = Entity.AsQueryable();

            foreach (var property in properties)
            {
                query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> GetEntityByIdAsync(int Id)
        {
            return await Entity.FindAsync(Id);
        }

        public virtual async Task DeleteAsync(int Id)
        {
            var entity = await Entity.FindAsync(Id);

            if (entity != null)
            {
                Entity.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<List<TEntity>?> AddRangeAsync(List<TEntity> entities)
        {
            await Entity.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }
        public async Task<TEntity> SaveEntityAsync(TEntity entity)
        {
            await Entity.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity?> UpdateEntityAsync(int id, TEntity entity)
        {
            var entry = await Entity.FindAsync(id);

            if (entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entry;
            }
            return null;
        }
    }
}
