using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IBasketService
{
    Task<IActionResult> GetBasket(int userId);
    Task<IActionResult> AddProduct(BasketQuery newbasket, int userId);
    Task<IActionResult> RemoveProduct(BasketQuery removedbasket, int userId);
    Task<IActionResult> OrderBasket(int id, int userId);

}