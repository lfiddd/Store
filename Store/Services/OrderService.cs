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

    public async Task<IActionResult> GetYourOrderAsync(string Authorization)
    {
        var selectedUser = _context.Sessions
            .FirstOrDefault(session => session.token == Authorization);
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
    

    public async Task<IActionResult> CancelOrderAsync(string Authorization,DeleteOrder deleteOrder)
    {
        var selectedUser = await _context.Sessions
            .FirstOrDefaultAsync(s => s.token == Authorization);
        
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.id_order == deleteOrder.id_order && o.id_user == selectedUser.id_user);

        if (order == null)
            return new NotFoundObjectResult(new { status = false, message = "Order not founded!" });

        if (order.id_status != 1)
            return new BadRequestObjectResult(new { status = false, message = "Order status in not prepairing" });

        var basket = await _context.Baskets
            .FirstOrDefaultAsync(b => b.id_order == order.id_order);

        if (basket != null)
        {
            basket.id_order = null;     
            basket.IsOrdered = false;  
            _context.Update(basket);
            await _context.SaveChangesAsync();
        }

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();
        return new OkObjectResult(new { status = true, message = "Order has been cancelled!" });
    }

    public async Task<IActionResult> ChangeYourMindSet(string Authorization, ChangeMindSet changeOrder)
    {
        var selectedUser = _context.Sessions
            .FirstOrDefault(session => session.token == Authorization);
        
        var uOrder = await _context.Orders.FirstOrDefaultAsync(o => o.id_user == selectedUser.id_user && o.id_order == changeOrder.order_id);

        if (uOrder.id_status != 1)
        {
            return new NotFoundObjectResult(new { status = false, message = "Order is delivering!" });
        }
        uOrder.DeliveryAddress = changeOrder.DeliveryAddress;
        uOrder.id_paytype = changeOrder.PaymentType;
        _context.Update(uOrder);
        await _context.SaveChangesAsync();
        return new OkObjectResult(new { status = true, message = "Success" });
    }
}