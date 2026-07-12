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
    public class CategoryRepositry : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepositry(ApplicationDbContext context) : base(context)
        {
        }
    }
}
