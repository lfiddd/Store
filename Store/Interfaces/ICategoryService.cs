using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface ICategoryService
{
    Task<IActionResult> GetCategories([FromHeader]string Authorization);
    Task<IActionResult> CreateCategory([FromHeader]string Authorization, CategoryQuery category);
    Task<IActionResult> UpdateCategory([FromHeader]string Authorization, int id, CategoryQuery category);
    Task<IActionResult> DeleteCategory([FromHeader]string Authorization, int id);
}