using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Models;
using Store.Requests;

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

    public async Task<IActionResult> CreateNewUserAndLogin(UserQuery newUser)
    {
        var newLogin = new Login()
        {
            User = new User()
            {
                FullName = newUser.FullName,
                Email = newUser.Email,
                Address = newUser.Address,
                PhoneNumber = newUser.PhoneNumber,
                id_role = 3,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            },
            UserLogin = newUser.UserLogin,
            Password = newUser.Password
        };
        
        await _context.AddAsync(newLogin);
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "User created"
        });
    }

    public async Task<IActionResult> UpdateUserAndLogin(int id, UserQuery updatedUser)
    {
        var selectedUser = _context.Logins.Include(l => l.User).FirstOrDefault(l => l.id_user == id);
        if (selectedUser == null)
        {
            return new NotFoundObjectResult(new{status = false, message = "User not found"});
        }
        
        selectedUser.UserLogin = updatedUser.UserLogin;
        selectedUser.Password = updatedUser.Password;
        selectedUser.User.FullName = updatedUser.FullName;
        selectedUser.User.Email = updatedUser.Email;
        selectedUser.User.PhoneNumber = updatedUser.PhoneNumber;
        selectedUser.User.Address = updatedUser.Address;
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
             status = true,
             message = "User updated"
        });
    }

    public async Task<IActionResult> DeleteUser(int id)
    {
        var selectedUser = _context.Logins.Include(l => l.User).FirstOrDefault(l => l.id_user == id);
        
        if (selectedUser == null)
        {
            return new NotFoundObjectResult(new { status = false, message = "Login not found." });
        }

        _context.Logins.Remove(selectedUser);

        if (selectedUser.User != null)
        {
            _context.Users.Remove(selectedUser.User);
        }

        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            status = true,
            message = "User and login deleted successfully."
        });
    }
}