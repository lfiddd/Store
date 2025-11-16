using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IOrderService
{
    Task<IActionResult> GetAllOrdersAsync();
    Task<IActionResult> GetYourOrderAsync();
    Task<IActionResult> CancelOrderAsync(int id);
    Task<IActionResult> ChangeYourMindSet(int id, OrderQuery changeOrder);
}