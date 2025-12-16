using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Perpustakaan.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _table;
        public GenericRepository(AppDbContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void DeleteAsync(T entity)
        {
            _table.Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id) ?? throw new Exception("Data not found");
        }

        public void UpdateAsync(T entity)
        {
            _table.Update(entity);
        }
    }
}
