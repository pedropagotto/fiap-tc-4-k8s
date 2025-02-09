using System.Diagnostics.CodeAnalysis;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<T> : IRepository<T> where T : EntityBase
    {
        protected AppDbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> Delete(int id)
        {
            var entity = await GetById(id);

            if (entity is null) return 0;

            _dbSet.Remove(entity);
            var totalDeleted = await _context.SaveChangesAsync();

            return totalDeleted;
        }

        public async Task<T>? GetById(int id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task<IList<T>> ListAll() => await _dbSet.ToListAsync();

    }
}
