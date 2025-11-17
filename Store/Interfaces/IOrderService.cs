using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IOrderService
{
    Task<IActionResult> GetAllOrdersAsync();
    Task<IActionResult> GetYourOrderAsync(int userid);
    Task<IActionResult> CancelOrderAsync(int id);
    Task<IActionResult> ChangeYourMindSet(int id, OrderQuery changeOrder);
}