using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IProductService
{
    Task<IActionResult> GetAllProducts([FromHeader]string Authorization);
    Task<IActionResult> CreateNewProduct([FromHeader]string Authorization, ProductQuery newProduct);
    Task<IActionResult> UpdateProduct([FromHeader]string Authorization, int id, ProductQuery updatedproduct);
    Task<IActionResult> DeleteProduct([FromHeader]string Authorization ,int id);
}