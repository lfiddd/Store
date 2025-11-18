using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IBasketService
{
    Task<IActionResult> GetBasket([FromHeader] string Authorization);
    Task<IActionResult> AddProduct([FromBody] BasketQuery newbasket,[FromHeader] string Authorization);
    Task<IActionResult> RemoveProduct([FromHeader] BasketQuery removedbasket, [FromHeader]string Authorization);
    Task<IActionResult> OrderBasket([FromHeader] string Authorization, OrderQuery order);

}