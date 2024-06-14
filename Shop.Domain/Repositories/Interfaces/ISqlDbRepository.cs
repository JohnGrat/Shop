using System.Linq.Expressions;


namespace Shop.Domain.Repositories.Interfaces
{
    public interface ISqlDbRepository
    {
        Task<bool> AnyAsync<T>() where T : class;
        Task DeleteAsync<T>(Guid id) where T : class;
        Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        Task<T> GetByIdAsync<T>(Guid id) where T : class;
        Task InsertAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(Guid id, T entity) where T : class;
    }
}
