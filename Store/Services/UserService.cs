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
    
    public async Task<IActionResult> GetAllUsers(string Authorization, int id_role)
    {
         var users = await _context.Users.Where(u => u.id_role == id_role).ToListAsync();
         
         if (users == null)
         {
             return new NotFoundObjectResult(new { status = false, message = "Users not found." });
         }
         
         var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
         _context.ActionLogs.Add(new ActionLogs()
         {
             action_date = DateOnly.FromDateTime(DateTime.UtcNow),
             id_user = thisUser.id_user,
             id_action = 21
         });
         await _context.SaveChangesAsync();
         
         return new OkObjectResult(new
         {
             data = new {users = users},
             status = true
         });
    }
    
    public async Task<IActionResult> CreateNewUserAndLogin(string Authorization, UserQuery newUser, int id_role )
    {
        var newLogin = new Login()
        {
            User = new User()
            {
                FullName = newUser.FullName,
                Email = newUser.Email,
                Address = newUser.Address,
                PhoneNumber = newUser.PhoneNumber,
                id_role = id_role,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            },
            UserLogin = newUser.UserLogin,
            Password = newUser.Password
        };
        
        
        
        await _context.AddAsync(newLogin);
        await _context.SaveChangesAsync();
        
        var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 24
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "User created"
        });
    }

    public async Task<IActionResult> UpdateUserAndLogin(string Authorization, int id, UserQuery updatedUser)
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
        
        var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 25
        });
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
             status = true,
             message = "User updated"
        });
    }

    public async Task<IActionResult> DeleteUser(string Authorization, int id)
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

        var thisUser = await _context.Sessions.Include(l => l.User).FirstOrDefaultAsync(u => u.token == Authorization);
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = thisUser.id_user,
            id_action = 26
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "User and login deleted successfully."
        });
    }

    public async Task<IActionResult> RegitrationUser(UserQuery registrationUser)
    {
        var newLogin = new Login()
        {
            User = new User()
            {
                FullName = registrationUser.FullName,
                Email = registrationUser.Email,
                Address = registrationUser.Address,
                PhoneNumber = registrationUser.PhoneNumber,
                id_role = 3,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            },
            UserLogin = registrationUser.UserLogin,
            Password = registrationUser.Password
        };
        
        await _context.AddAsync(newLogin);
        await _context.SaveChangesAsync();
        
        _context.ActionLogs.Add(new ActionLogs()
        {
            action_date = DateOnly.FromDateTime(DateTime.UtcNow),
            id_user = newLogin.User.id_user,
            id_action = 1 
        });
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Regitration is successfully."
        });
    }

    public async Task<IActionResult> AuthorizationAsync(AuthUser authUser)
    {
        var selectedUser = _context.Logins
            .Include(login => login.User)
            // .ThenInclude(user => user.Role)
            .FirstOrDefault(login => login.UserLogin == authUser.UserLogin && login.Password == authUser.Password);

        if (selectedUser != null)
        {
            string token = _jwtTokensGenerator.GenerateJwtToken(selectedUser.id_user, selectedUser.User.id_role);

            _context.Sessions.Add(new Session()
            {
                token = token,
                id_user = selectedUser.id_user,
            });
            await _context.SaveChangesAsync();
            
            _context.ActionLogs.Add(new ActionLogs()
            {
                action_date = DateOnly.FromDateTime(DateTime.UtcNow),
                id_user = selectedUser.User.id_user,
                id_action = 2
            });
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { status = true, data = token, selectedUser.id_user });
        }
        else
        {
            return new NotFoundObjectResult(new
                { status = false, message = "User not found. Check you login and password!" });
        }
    }

    public async Task<IActionResult> GetUserProfile(string Authorization)
    {
        var selectedUser = _context.Sessions.Include(l => l.User).FirstOrDefault(l => l.token == Authorization);
        if (selectedUser != null)
        {
            _context.ActionLogs.Add(new ActionLogs()
            {
                action_date = DateOnly.FromDateTime(DateTime.UtcNow),
                id_user = selectedUser.id_user,
                id_action = 3
            });
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { status = true, selectedUser = selectedUser });
        }
        else
        {
            return new NotFoundObjectResult(new
                { status = false, message = "User not founded!" });
        }
    }

    public async Task<IActionResult> UpdateUserProfile(string Authorization, UserQuery updatedProfile)
    {
        var selectedSession = _context.Sessions.FirstOrDefault(session => session.token == Authorization);

        if (selectedSession != null)
        {
            var userLogin = await _context.Logins
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.id_user == selectedSession.id_user);

            if (userLogin == null)
            {
                return new NotFoundObjectResult(new { status = false, message = "Login not found." });
            }

            userLogin.UserLogin = updatedProfile.UserLogin;
            userLogin.Password = updatedProfile.Password;
            userLogin.User.FullName = updatedProfile.FullName;
            userLogin.User.Email = updatedProfile.Email;
            userLogin.User.PhoneNumber = updatedProfile.PhoneNumber;
            userLogin.User.Address = updatedProfile.Address;
            userLogin.User.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);

            await _context.SaveChangesAsync();
            
            _context.ActionLogs.Add(new ActionLogs()
            {
                action_date = DateOnly.FromDateTime(DateTime.UtcNow),
                id_user = userLogin.User.id_user,
                id_action = 3
            });
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "User and login edited successfully."
            });
        }
        else
        {
            return new NotFoundObjectResult(new
                { status = false, message = "Session not founded!" });
        }
    }
}