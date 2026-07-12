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

    

         async Task<CategoryResponse> ICategoryService.CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>(); 
        }
        public async Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request)
        {
            var category = await _categoryRepository.GetOne(
                c => c.Id == id,
                new[] { nameof(Category.Translations) });

            if (category == null)
                throw new Exception("Category not found");

            foreach (var translationRequest in request.Translations)
            {
                var translation = category.Translations
                    .FirstOrDefault(t => t.Language == translationRequest.Language);

                if (translation != null)
                {
                    // تعديل الترجمة الموجودة
                    translation.Name = translationRequest.Name;
                }
                else
                {
                    // إضافة ترجمة جديدة إذا لم تكن موجودة
                    category.Translations.Add(new CategoryTranslation
                    {
                        Name = translationRequest.Name,
                        Language = translationRequest.Language
                    });
                }
            }

            await _categoryRepository.UpdateAsync(category);

            return category.Adapt<CategoryResponse>();
        }
        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync(
                new string[] { nameof(Category.Translations) });
            return categories.Adapt<List<CategoryResponse>>();
        }
        public async Task<CategoryResponse> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var category = await _categoryRepository.GetOne(
             filter, new string[] { nameof(Category.Translations) });
            return category.Adapt<CategoryResponse>();
        }

        public async Task<bool> DeletedCategory(int id)
        {
            var category = await _categoryRepository.GetOne(c => c.Id == id);
            if (category == null) return false;
            return await _categoryRepository.DeleteAsync(category);
        }



    }
}
