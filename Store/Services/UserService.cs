using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;

namespace Store.Services;

public class UserService : IUserService
{
    private readonly ContextDatabase _context;
    private IUserService _userService;
    
    public async Task<IActionResult> GetAllUsers()
    {
         var users = _context.Users.ToListAsync();
         
         return new OkObjectResult(new
         {
             data = new {users = users},
             status = true
         });
    }

    public Task<IActionResult> GetUserById(string id)
    {
        throw new NotImplementedException();
    }
}