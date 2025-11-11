using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store.Requests;

namespace Store.Services;

public class OrderService : IOrderService
{
    public Task<IActionResult> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> CreateOrderAsync(OrderQuery newOrder)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> CancelOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> ChangeYourMindSet(int id, OrderQuery changeOrder)
    {
        throw new NotImplementedException();
    }
}