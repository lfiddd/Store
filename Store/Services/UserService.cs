using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;
using Store.Models;
using Store.Requests;
using Store.UniversalMethods;

namespace Store.Services;

public class UserService : IUserService
{
    private readonly ContextDatabase _context;
    private readonly JWTTokensGenerator _jwtTokensGenerator;
    
    public UserService(ContextDatabase contextDatabase, JWTTokensGenerator jwtTokensGenerator)
    {
        _context = contextDatabase;
        _jwtTokensGenerator = jwtTokensGenerator;
    }
    
    public async Task<IActionResult> GetAllUsers(int id_role)
    {
         var users = await _context.Users.ToListAsync();
         
         return new OkObjectResult(new
         {
             data = new {users = users},
             status = true
         });
    }
    
    public async Task<IActionResult> CreateNewUserAndLogin(UserQuery newUser, int id_role)
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

    public Task<IActionResult> AuthorizationAsync(AuthUser authUser)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetUserProfile(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> UpdateUserProfile(string userId, UserQuery updatedProfile)
    {
        throw new NotImplementedException();
    }

}