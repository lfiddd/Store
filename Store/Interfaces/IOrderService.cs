using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IOrderService
{
    Task<IActionResult> GetAllOrdersAsync([FromHeader]string Authorization);
    Task<IActionResult> GetYourOrderAsync([FromHeader]string Authorization);
    Task<IActionResult> CancelOrderAsync([FromHeader]string Authorization, DeleteOrder deleteOrder);
    Task<IActionResult> ChangeYourMindSet([FromHeader]string Authorization, ChangeMindSet changeOrder);
}