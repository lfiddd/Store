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
    public async Task<IActionResult> GetBasket([FromHeader]int userId) => await _basketService.GetBasket(userId);
    
    [HttpPost("AddProduct")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> AddProduct([FromHeader]BasketQuery newbasket, int userId) => await _basketService.AddProduct(newbasket, userId);
    
    [HttpPut("DeleteProduct")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> RemoveProduct([FromHeader]BasketQuery removedbasket, int userId) => await _basketService.RemoveProduct(removedbasket, userId);
    
    [HttpPut("OrderBasket")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> OrderBasket([FromHeader]int id, int userId) => await _basketService.OrderBasket(id, userId);
 
    
}