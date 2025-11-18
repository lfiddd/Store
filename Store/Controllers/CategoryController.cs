using Microsoft.AspNetCore.Mvc;
using Store.CustomAtributes;
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
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> GetAllCategories([FromHeader]string Authorization) => await _categoryService.GetCategories(Authorization);
    
    [HttpPost("CreateCategory")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> CreateCategory(CategoryQuery category ,[FromHeader]string Authorization) => await _categoryService.CreateCategory(category ,Authorization);
    
    [HttpPut("UpdateCategory")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateCategory(int id, CategoryQuery category ,[FromHeader]string Authorization) => await _categoryService.UpdateCategory(id, category ,Authorization);
    
    [HttpDelete("DeleteCategory")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteCategory(int id ,[FromHeader]string Authorization) => await _categoryService.DeleteCategory(id ,Authorization);
}