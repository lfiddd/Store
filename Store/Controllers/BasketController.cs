using Microsoft.AspNetCore.Mvc;
using Store.CustomAtributes;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    public BasketController(IBasketService basketService) => _basketService = basketService;
    
    [HttpGet("GetBasket")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> GetBasket([FromHeader]string Authorization) => await _basketService.GetBasket(Authorization);
    
    [HttpPost("AddProduct")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> AddProduct([FromBody] BasketQuery newbasket,[FromHeader] string Authorization) => await _basketService.AddProduct(newbasket, Authorization);
    
    [HttpPut("DeleteProduct")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> RemoveProduct([FromBody] BasketQuery removedbasket, [FromHeader]string Authorization) => await _basketService.RemoveProduct(removedbasket, Authorization);
    
    [HttpPut("OrderBasket")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> OrderBasket([FromHeader] string Authorization, OrderQuery order) => await _basketService.OrderBasket(Authorization, order);
 
    
}