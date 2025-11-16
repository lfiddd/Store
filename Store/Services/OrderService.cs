using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Requests;

namespace Store.Services;

public class OrderService : IOrderService
{
    private readonly ContextDatabase _context;

    public OrderService(ContextDatabase contextDatabase)
    {
        _context = contextDatabase;
    }
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetYourOrderAsync()
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