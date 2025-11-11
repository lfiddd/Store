using Microsoft.AspNetCore.Mvc;
using Store.CustomAtributes;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService) => _productService = productService;
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => await _productService.GetAllProducts();
    
    [HttpPost]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> CreateNewProduct(ProductQuery newProduct) => await _productService.CreateNewProduct(newProduct);
    
    [HttpPut("{id}")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateProduct(int id, ProductQuery updatedproduct) => await _productService.UpdateProduct(id, updatedproduct);

    [HttpDelete("{id}")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteProduct(int id) => await _productService.DeleteProduct(id);
}