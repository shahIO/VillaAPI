using MyVilla_API.Models;
using System.Linq.Expressions;

namespace MyVilla_API.Repository.IRepository
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
