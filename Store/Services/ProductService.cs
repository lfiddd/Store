using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Models;
using Store.Requests;

namespace Store.Services;

public class ProductService : IProductService
{
    private readonly ContextDatabase _context;

    public ProductService(ContextDatabase contextDatabase)
    {
        _context = contextDatabase;
    }
    public async Task<IActionResult> GetAllProducts(string Authorization)
    {
        var product = await _context.Products.ToListAsync();
        
        var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 5
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            data = new {products = product}
        });
    }
    
    public async Task<IActionResult> CreateNewProduct(string Authorization ,ProductQuery newProduct)
    {
        var newProd = new Product()
        {
            NameProduct = newProduct.NameProduct,
            Description = newProduct.Description,
            Price = newProduct.Price,
            Stock = newProduct.Stock,
            IsActive = newProduct.IsActive,
            CreatedAt = newProduct.CreatedAt,
            id_category = newProduct.id_category,
        };
        
        await _context.AddAsync(newProd);
        await _context.SaveChangesAsync();
        
        var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 6
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Product created successfully."
        });
    }

    public async Task<IActionResult> UpdateProduct(string Authorization, int id, ProductQuery updatedproduct)
    {
        var selectedProduct = _context.Products.FirstOrDefault(p => p.id_product == id);
        
        if (selectedProduct == null)
        {
            return new NotFoundObjectResult(new{status = false, message = "Product not found"});
        }
        
        selectedProduct.NameProduct = updatedproduct.NameProduct;
        selectedProduct.Description = updatedproduct.Description;
        selectedProduct.Price = updatedproduct.Price;
        selectedProduct.Stock = updatedproduct.Stock;
        selectedProduct.IsActive = updatedproduct.IsActive;
        selectedProduct.CreatedAt = updatedproduct.CreatedAt;
        selectedProduct.id_category = updatedproduct.id_category;
        await _context.SaveChangesAsync();
        
        var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 7
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Product updated"
        });
    }

    public async Task<IActionResult> DeleteProduct(string Authorization ,int id)
    {
        var selectedProduct = _context.Products.FirstOrDefault(p => p.id_product == id);
        
        if (selectedProduct == null)
        {
            return new NotFoundObjectResult(new { status = false, message = "Product not found." });
        }
        
        _context.Products.Remove(selectedProduct);
        await _context.SaveChangesAsync();
        
        var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
        await _context.ActionLogs.AddAsync(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 8
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Product deleted successfully."
        });
    }
}