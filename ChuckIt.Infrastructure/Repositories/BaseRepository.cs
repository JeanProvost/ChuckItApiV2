using ChuckIt.Core.Interfaces.IRepositories;
using ChuckItApiV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChuckIt.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<T> Dbset => _context.Set<T>();

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<T> Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task Delete(T model)
        {
            _context.Set<T>().Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
