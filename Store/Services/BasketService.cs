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
    public async Task<IActionResult> GetBasket(int userId)
    {
        var selectedSession = _context.Sessions.FirstOrDefault(session => session.id_user == userId);
        if (selectedSession != null)
        {
            var basketItems = await _context.BasketItems.Where(bi => bi.Basket.IsOrdered == false).ToListAsync();

            return new OkObjectResult(new
            {
                status = true,
                data = basketItems
            });
        }
        else return new NotFoundObjectResult(new { status = false, message = "Session not found" });
    }
    
    public async Task<IActionResult> AddProduct(BasketQuery query, int userId)
    { 
        var selectedSession = _context.Sessions.FirstOrDefault(session => session.id_user == userId);
        if (selectedSession != null)
        {
            var newBasket = new Basket()
            {
                IsOrdered = false,
                id_user = userId,
                id_order = null
            };
            await _context.AddAsync(newBasket);
            await _context.SaveChangesAsync();
            
            var priceProd = await _context.BasketItems.Include(bi => bi.Product).FirstOrDefaultAsync(bi => bi.Basket.id_user == userId);
            
            foreach (var _id_product in query.id_product)
            {
                var baskItem = _context.BasketItems.FirstOrDefault(bi => bi.id_product == _id_product);
                if (baskItem != null)
                {
                    baskItem.ProdCount += 1;
                    _context.Update(baskItem);
                }
                else
                {
                    var newBasketItem = new BasketItem()
                    {
                        id_product = _id_product,
                        ProdCount = 1,
                        id_basket = newBasket.id_basket
                    };
                    await _context.AddAsync(newBasketItem);
                }
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Item added successfully"
            });
        }
        else return new NotFoundObjectResult(new{status = false, message="Session not found"});
    }

    public async Task<IActionResult> RemoveProduct(BasketQuery removedbasket, int userId)
    {
        var selectedSession = _context.Sessions.FirstOrDefault(session => session.id_user == userId);
        if (selectedSession != null)
        {
            var priceProd = await _context.BasketItems.Include(bi => bi.Product).FirstOrDefaultAsync(bi => bi.Basket.id_user == userId);
            
            foreach (var _id_product in removedbasket.id_product)
            {
                var baskItem = _context.BasketItems.FirstOrDefault(bi => bi.id_product == _id_product);
                if (baskItem.ProdCount > 1)
                {
                    baskItem.ProdCount -= 1;
                    _context.Update(baskItem);
                }
                else if (baskItem.ProdCount == 1)
                {
                    _context.Remove(baskItem);
                }
                else return new NotFoundObjectResult(new { status = false, message = "Product not found" });
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(new { status = true, message = "Product removed successfully" });
        }
        else return new NotFoundObjectResult(new { status = false, message = "Session not found" });
    }

    public async Task<IActionResult> OrderBasket(int id, int userId)
    {
        var selectedSession = _context.Sessions.FirstOrDefault(session => session.id_user == userId);
        if (selectedSession != null)
        {
            var selectedBasket = _context.Baskets.Where(b => b.id_user == userId).FirstOrDefaultAsync(b => b.IsOrdered == false);
            
            return new OkObjectResult(new { status = true, message = "Ordering successfully" });
        }
        else return new NotFoundObjectResult(new { status = false, message = "Session not found" });
    }

}