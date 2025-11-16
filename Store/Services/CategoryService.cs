using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Models;
using Store.Requests;

namespace Store.Services;

public class CategoryService : ICategoryService
{
    private readonly ContextDatabase _context;

    public CategoryService(ContextDatabase contextDatabase)
    {
        _context = contextDatabase;
    }
    public async Task<IActionResult> GetCategories()
    {
        var productCategories = await _context.ProductCategories.ToListAsync();

        return new OkObjectResult(new
        {
            status = true,
            data = new { productCategories = productCategories }
        });
    }

    public async Task<IActionResult> CreateCategory(CategoryQuery category)
    {
        var existingCategory = await _context.ProductCategories
            .FirstOrDefaultAsync(c => c.NameCategory == category.NameCategory);
            
        if (existingCategory != null)
            return new BadRequestObjectResult(new { status = false, message = "Категория с таким названием уже существует" });

        var newCategory = new ProductCategories()
        {
            NameCategory = category.NameCategory
        };

        await _context.ProductCategories.AddAsync(newCategory);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            status = true,
            message = "New category created",
        });
    }

    public async Task<IActionResult> UpdateCategory(int id, CategoryQuery category)
    {
        var selectedCategory = await _context.ProductCategories
            .FirstOrDefaultAsync(c => c.id_category == id);
            
        if (category == null)
            return new NotFoundObjectResult(new { status = false, message = "Category not found" });

        selectedCategory.NameCategory = category.NameCategory;
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            status = true,
            message = "Category updated",
        });
    }

    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.ProductCategories
            .FirstOrDefaultAsync(c => c.id_category == id);

        if (category == null)
            return new NotFoundObjectResult(new { status = false, message = "Category not found" });
            
        var productsInCategory = await _context.Products
            .AnyAsync(p => p.id_category == id);

        if (productsInCategory)
            return new BadRequestObjectResult(new
                { status = false, message = "Unable to delete category, because it is in use" });

        _context.ProductCategories.Remove(category);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            status = true,
            message = "Category deleted"
        });
    }
}