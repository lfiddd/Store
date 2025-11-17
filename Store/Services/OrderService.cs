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
        var allOrders  = await _context.Orders.ToListAsync();

        return new OkObjectResult(new
        {
            status = true,
            data = new { orders = allOrders }
        });
    }

    public async Task<IActionResult> GetYourOrderAsync(int userid)
    {
        var selectedUser = _context.Sessions
            .FirstOrDefault(session => session.id_user == userid);
        if (selectedUser != null)
        {
            var uOrder = await _context.Orders.FirstOrDefaultAsync(o => o.id_user == selectedUser.id_user);
            return new OkObjectResult(new { status = true, data = new { order = uOrder } });
        }
        else
        {
            return new NotFoundObjectResult(new
                { status = false, message = "Session not founded!" });
        }
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