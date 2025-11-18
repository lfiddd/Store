using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Models;
using Store.Requests;

namespace Store.Services;

public class BasketService: IBasketService
{
    private readonly ContextDatabase _context;
    public BasketService(ContextDatabase contextDatabase)
    {
        _context = contextDatabase;
    }
    public async Task<IActionResult> GetBasket(string Authorization)
    {
        var selecteduser = _context.Sessions.Include(s => s.User).FirstOrDefault(session => session.token == Authorization);
        var basketItem = await _context.BasketItems.Where(bi => bi.Basket.IsOrdered == false && bi.Basket.id_user == selecteduser.id_user).ToListAsync();
        
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = selecteduser.id_user,
            id_action = 14
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            data = new {basketItems = basketItem},
        });
    }
    
    public async Task<IActionResult> AddProduct(BasketQuery query, string Authorization)
    { 
        var thisUser = await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.token == Authorization);
        
        var basket = await _context.Baskets
            .FirstOrDefaultAsync(b => b.id_user == thisUser.id_user && b.IsOrdered == false);

        if (basket == null)
        {
            basket = new Basket
            {
                id_user = thisUser.id_user,
                IsOrdered = false,
                id_order = null
            };
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync(); 
        }

        var basketItem = await _context.BasketItems
            .FirstOrDefaultAsync(bi => bi.id_basket == basket.id_basket && bi.id_product == query.id_product);

        if (basketItem == null)
        {
            basketItem = new BasketItem
            {
                id_basket = basket.id_basket,
                id_product = query.id_product,
                ProdCount = 1
            };
           await _context.AddAsync(basketItem);
        }
        else
        {
            basketItem.ProdCount += 1;
            _context.Update(basketItem);
        }

        await _context.SaveChangesAsync();
        
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 13
        });
        await _context.SaveChangesAsync();

        return new OkObjectResult(new { status = true, message = "Success" });
    }

    public async Task<IActionResult> RemoveProduct(BasketQuery query, string Authorization)
    {
        var thisUser = await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.token == Authorization);
        
        var basket = await _context.Baskets
            .FirstOrDefaultAsync(b => b.id_user == thisUser.id_user && b.IsOrdered == false);
        
        var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.id_product == query.id_product && b.id_basket == basket.id_basket);

        if (basketItem.ProdCount > 1)
        {
            basketItem.ProdCount -= 1;
            _context.Update(basketItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            _context.Remove(basketItem);
            await _context.SaveChangesAsync();
        }

        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 15
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new { status = true, message = "Success" });
    }

    public async Task<IActionResult> OrderBasket(string Authorization, OrderQuery order)
    {
        var thisUser = await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.token == Authorization);

        var basket = await _context.Baskets
            .FirstOrDefaultAsync(b => b.id_user == thisUser.id_user && !b.IsOrdered);

        if (basket == null)
            return new NotFoundObjectResult(new { status = false, message = "Корзина не найдена или уже оформлена" });

        var newOrder = new Order
        {
            OrderDate = DateOnly.FromDateTime(DateTime.Now),
            id_user = thisUser.id_user,
            id_status = order.OrderStatus,
            id_delivtype = order.DeliveryType,
            id_paytype = order.PaymentType,
            DeliveryAddress = order.DeliveryAddress
        };

        _context.Orders.Add(newOrder);

        await _context.SaveChangesAsync();
        
        basket.id_order = newOrder.id_order;  
        basket.IsOrdered = true;
        _context.Update(basket);
        await _context.SaveChangesAsync();
        
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 16
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new { status = true, message = "Success" });
    }

}