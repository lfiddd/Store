using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IBasketService
{
    Task<IActionResult> GetBasket([FromHeader]int userId);
    Task<IActionResult> AddProduct([FromHeader]BasketQuery newbasket, int userId);
    Task<IActionResult> RemoveProduct([FromHeader]BasketQuery removedbasket, int userId);
    Task<IActionResult> OrderBasket([FromHeader]int id, int userId);

}