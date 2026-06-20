using KAshop.DAL.Data;
using KAshop.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.Repository
{
    public class CategoryRepositry : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepositry(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<List<Category>> GetAllAsync()
        {
            var categories =await  _context.Categories.Include(c => c.Translations).ToListAsync();
            return categories;
        }
    }
}
