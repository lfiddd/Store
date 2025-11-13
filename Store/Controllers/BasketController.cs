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
    public async Task<IActionResult> GetBasket(int userId) => await _basketService.GetBasket(userId);
    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct(BasketQuery newbasket, int userId) => await _basketService.AddProduct(newbasket, userId);
    [HttpPut("DeleteProduct")]
    public async Task<IActionResult> RemoveProduct(BasketQuery removedbasket, int userId) => await _basketService.RemoveProduct(removedbasket, userId);
    [HttpPut("OrderBasket")]
    public async Task<IActionResult> OrderBasket(int id, int userId) => await _basketService.OrderBasket(id, userId);
 
    
}