using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface ICategoryService
{
    Task<IActionResult> GetCategories();
    Task<IActionResult> CreateCategory(CategoryQuery category);
    Task<IActionResult> UpdateCategory(int id, CategoryQuery category);
    Task<IActionResult> DeleteCategory(int id);
}