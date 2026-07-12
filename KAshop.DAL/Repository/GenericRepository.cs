using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAshop.DAL.Data;
using System.Linq.Expressions;
namespace KAshop.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }
        public async Task<T> UpdateAsync(T request)
        {
            _context.Set<T>().Update(request);
            await _context.SaveChangesAsync();
            return request;
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<List<T>> GetAllAsync(string [] ? includes = null )
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();


        }

        public async Task<T> GetOne(Expression<Func<T, bool>> filter, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(filter);

        }

       
    }
}
