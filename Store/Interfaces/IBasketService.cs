using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IBasketService
{
    Task<IActionResult> GetBasket();
    Task<IActionResult> AddProduct(BasketQuery newbasket);
    Task<IActionResult> RemoveProduct(BasketQuery removedbasket);
    Task<IActionResult> OrderBasket(int id);

}