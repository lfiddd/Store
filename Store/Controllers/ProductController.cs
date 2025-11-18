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
    public async Task<IActionResult> CreateNewProduct([FromHeader]string Authorization, ProductQuery newProduct) => await _productService.CreateNewProduct(Authorization, newProduct);
    
    [HttpPut("{id}")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateProduct([FromHeader]string Authorization, int id, ProductQuery updatedproduct ) => await _productService.UpdateProduct(Authorization, id, updatedproduct);

    [HttpDelete("{id}")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteProduct([FromHeader]string Authorization, int id ) => await _productService.DeleteProduct(Authorization, id);
}