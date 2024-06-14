using Microsoft.EntityFrameworkCore;
using Shop.Domain.Repositories.Interfaces;
using Shop.Infrastructure.Persistence.SqlDb;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories
{
    public class SqlDbRepository : ISqlDbRepository
    {
        private readonly SqlDbContext _dbContext;

        public SqlDbRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AnyAsync<T>() where T : class
        {
            return await _dbContext.Set<T>().AnyAsync();
        }

        public async Task DeleteAsync<T>(Guid id) where T : class
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync<T>(T entity) where T : class
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(Guid id, T entity) where T : class
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
