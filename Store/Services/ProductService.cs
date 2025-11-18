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
    public async Task<IActionResult> GetAllProducts()
    {
        var product = await _context.Products.ToListAsync();
        
        
        return new OkObjectResult(new
        {
            status = true,
            data = new {products = product}
        });
    }
    
    public async Task<IActionResult> CreateNewProduct(ProductQuery newProduct)
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
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Product created successfully."
        });
    }

    public async Task<IActionResult> UpdateProduct(int id, ProductQuery updatedproduct)
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
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Product updated"
        });
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        var selectedProduct = _context.Products.FirstOrDefault(p => p.id_product == id);
        
        if (selectedProduct == null)
        {
            return new NotFoundObjectResult(new { status = false, message = "Product not found." });
        }
        
        _context.Products.Remove(selectedProduct);
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Product deleted successfully."
        });
    }
}