using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IProductService
{
    Task<IActionResult> GetAllProducts();
    Task<IActionResult> CreateNewProduct(ProductQuery newProduct);
    Task<IActionResult> UpdateProduct(int id, ProductQuery updatedproduct);
    Task<IActionResult> DeleteProduct(int id);
}