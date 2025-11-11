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
    public async Task<IActionResult> GetBasket()
    {
        var basket = await _context.Baskets.ToListAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            data = new{basket = basket}
        });
    }

    public async Task<IActionResult> AddProduct(BasketQuery query)
    {
        var newBasket = new Basket()
        {
            ProdCount = query.ProdCount,
            ResultPrice = query.ResultPrice,
            IsOrdered = false,
            id_user = 1,
            id_order = null
        };
        await _context.AddAsync(newBasket);
        await _context.SaveChangesAsync();

        foreach (var _id_product in query.id_product)
        {
            var newBasketItem = new BasketItem()
            {
                id_product = _id_product,
                id_basket = newBasket.id_basket,
            };
            await _context.AddAsync(newBasketItem);
        }
        
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Item added successfully"
        });
    }

    public Task<IActionResult> RemoveProduct(BasketQuery removedbasket)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> OrderBasket(int id)
    {
        throw new NotImplementedException();
    }

}