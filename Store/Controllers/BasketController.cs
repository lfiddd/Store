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
    
    [HttpGet]
    public async Task<IActionResult> GetBasket() => await _basketService.GetBasket();
    [HttpPost]
    public async Task<IActionResult> AddProduct(BasketQuery newbasket) => await _basketService.AddProduct(newbasket);
    [HttpDelete]
    public async Task<IActionResult> RemoveProduct(BasketQuery removedbasket) => await _basketService.RemoveProduct(removedbasket);
    [HttpPut]
    public async Task<IActionResult> OrderBasket(int id) => await _basketService.OrderBasket(id);
 
    
}