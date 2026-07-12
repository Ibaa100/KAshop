using KAshop.BLL.Services;
using KAshop.DAL.Data;
using KAshop.DAL.Dto;
using KAshop.DAL.Models;
using KAshop.DAL.Repository;
using KAshop.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KAshop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _categoryService;
        public CategoriesController(IStringLocalizer<SharedResources> localizer,ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }


        [HttpGet("")]
        public async Task<IActionResult> Index() {
            var categories = await _categoryService.GetAllCategories();

            return Ok(new { _localizer["Success"].Value, categories });
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var response = await _categoryService.CreateCategory(request);
            return Ok();

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request)
        {
            var response = await _categoryService.UpdateCategory(id, request);

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategory(c =>c.Id ==id);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var deleted = await _categoryService.DeletedCategory(id);
            if (!deleted)
                return BadRequest();
            return Ok();
        }
    }
}
