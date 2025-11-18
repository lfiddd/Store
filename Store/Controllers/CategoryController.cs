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
    public async Task<IActionResult> CreateCategory([FromHeader]string Authorization, CategoryQuery category ) => await _categoryService.CreateCategory(Authorization, category );
    
    [HttpPut("UpdateCategory")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateCategory([FromHeader]string Authorization, int id, CategoryQuery category ) => await _categoryService.UpdateCategory(Authorization, id, category );
    
    [HttpDelete("DeleteCategory")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteCategory([FromHeader]string Authorization, int id ) => await _categoryService.DeleteCategory(Authorization, id );
}