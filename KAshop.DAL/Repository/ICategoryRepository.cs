using KAshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.DAL.Repository
{
    public interface ICategoryRepository
    {
        Task <List<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category category);
    }
}
