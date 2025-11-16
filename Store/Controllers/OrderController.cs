using Microsoft.AspNetCore.Mvc;
using Store.CustomAtributes;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service) => _service = service;

    [HttpGet("order/all")]
    [RoleAtribute([1])]
    public async Task<IActionResult> GetAllOrders() => await _service.GetAllOrdersAsync();
    
    [HttpGet("GetYourOrder")]
    public async Task<IActionResult> GetYourOrder() => await _service.GetYourOrderAsync();

    [HttpPut("CancelOrder/{id}")]
    
    public async Task<IActionResult> CancelOrder(int id) => await _service.CancelOrderAsync(id);
    
    [HttpPut("order/changeYourMindSet")]
    
    public async Task<IActionResult> ChangeYourMindSet(int id, [FromBody] OrderQuery reader) => await _service.ChangeYourMindSet(id, reader);
}