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
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> GetAllOrders() => await _service.GetAllOrdersAsync();

    [HttpGet("GetYourOrder")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> GetYourOrder(int userid) => await _service.GetYourOrderAsync(userid);

    [HttpPut("CancelOrder/{id}")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> CancelOrder(int id) => await _service.CancelOrderAsync(id);
    
    [HttpPut("order/changeYourMindSet")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> ChangeYourMindSet(int id, [FromBody] OrderQuery reader) => await _service.ChangeYourMindSet(id, reader);
}