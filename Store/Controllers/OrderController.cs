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
    public async Task<IActionResult> GetAllOrders([FromHeader]string Authorization) => await _service.GetAllOrdersAsync(Authorization);

    [HttpGet("GetYourOrder")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> GetYourOrder([FromHeader]string Authorization) => await _service.GetYourOrderAsync(Authorization);

    [HttpDelete("CancelOrder")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> CancelOrder([FromHeader]string Authorization, DeleteOrder deleteOrder) => await _service.CancelOrderAsync(Authorization, deleteOrder);
    
    [HttpPut("order/changeYourMindSet")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> ChangeYourMindSet([FromHeader]string Authorization, [FromBody] ChangeMindSet changeOrder) => await _service.ChangeYourMindSet(Authorization, changeOrder);
}