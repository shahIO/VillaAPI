using Microsoft.EntityFrameworkCore;
using MyVilla_API.Data;
using MyVilla_API.Models;
using MyVilla_API.Repository.IRepository;
using System.Linq.Expressions;

namespace MyVilla_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public ApplicationDbContext _dbContext { get; }
        internal DbSet<T> dBSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dBSet=_dbContext.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dBSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dBSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dBSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dBSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
