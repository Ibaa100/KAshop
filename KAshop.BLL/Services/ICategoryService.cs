using KAshop.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.BLL.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> CreateCategory(CategoryRequest request);
    }
}
