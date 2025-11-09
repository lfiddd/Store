using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store.Requests;

namespace Store.Services;

public class BasketService: IBasketService
{
    public Task<IActionResult> GetBasket()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> CreateNewProduct(ProductQuery newProduct)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> UpdateProduct(int id, ProductQuery updatedproduct)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}