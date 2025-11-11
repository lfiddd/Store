using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IOrderService
{
    Task<IActionResult> GetAllOrdersAsync();
    Task<IActionResult> CreateOrderAsync(OrderQuery newOrder);
    Task<IActionResult> CancelOrderAsync(int id);
    Task<IActionResult> ChangeYourMindSet(int id, OrderQuery changeOrder);
}