using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal DashboardContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DashboardContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T?> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            List<T> list = await _dbSet.Where(expression).ToListAsync();

            return list.FirstOrDefault();
        }

        public async Task<List<T>> FindListAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }

}