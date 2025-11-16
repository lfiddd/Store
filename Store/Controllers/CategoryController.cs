using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet("GetCategories")]
    public async Task<IActionResult> GetAllCategories() => await _categoryService.GetCategories();
    
    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory(CategoryQuery category) => await _categoryService.CreateCategory(category);
    
    [HttpPut("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryQuery category) => await _categoryService.UpdateCategory(id, category);
    
    [HttpDelete("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory(int id) => await _categoryService.DeleteCategory(id);
}