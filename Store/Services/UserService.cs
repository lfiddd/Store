using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;

namespace Store.Services;

public class UserService : IUserService
{
    private readonly ContextDatabase _context;

    public UserService(ContextDatabase contextDatabase)
    {
        _context = contextDatabase;
    }
    
    public async Task<IActionResult> GetAllUsers()
    {
         var users = await _context.Users.ToListAsync();
         
         return new OkObjectResult(new
         {
             data = new {users = users},
             status = true
         });
    }

    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.id_user == id);

        if (user == null)
        {
            return new NotFoundObjectResult(new
            {
                status = false,
                message = "User not found"
            });
        }

        return new OkObjectResult(new
        {
            data = user,
            status = true
        });
    }

    public async Task<IActionResult> GetUserByName(string fullName)
    {
        var query = _context.Users.AsQueryable();
        
        if(!string.IsNullOrEmpty(fullName)) query = query.Where(u => EF.Functions.Like(u.FullName, $"%{fullName}%"));
        
        return new OkObjectResult(new
        {
            status = true,
            data = await query.ToListAsync()
        });
    }
}