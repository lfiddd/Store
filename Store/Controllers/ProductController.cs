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
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> GetAll([FromHeader]string Authorization) => await _productService.GetAllProducts(Authorization);
    
    [HttpPost]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> CreateNewProduct(ProductQuery newProduct, [FromHeader]string Authorization) => await _productService.CreateNewProduct(newProduct, Authorization);
    
    [HttpPut("{id}")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateProduct(int id, ProductQuery updatedproduct ,[FromHeader]string Authorization) => await _productService.UpdateProduct(id, updatedproduct, Authorization);

    [HttpDelete("{id}")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteProduct(int id ,[FromHeader]string Authorization) => await _productService.DeleteProduct(id, Authorization);
}