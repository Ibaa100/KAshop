using KAshop.DAL.Dto;
using KAshop.DAL.Models;
using KAshop.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories =await _categoryRepository.GetAllAsync(
                new string[] { nameof(Category.Translations)});
            return categories.Adapt<List<CategoryResponse>>();
        }
        public async Task<CategoryResponse> GetCategory(Expression<Func<Category,bool>> filter)
        {
            var category = await _categoryRepository.GetOne(
             filter,new string[] { nameof(Category.Translations) });
            return category.Adapt<CategoryResponse>();
        }

        async Task<CategoryResponse> ICategoryService.CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>(); 
        }
    }
}
