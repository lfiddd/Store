using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IUserService
{
    //Get tasks
    Task<IActionResult> GetAllUsers();
    
    //Add new user task
    Task<IActionResult> CreateNewUserAndLogin(UserQuery newUser);
    
    //Update user task
    Task<IActionResult> UpdateUserAndLogin(int id, UserQuery updatedUser);
    
    //Delete user task
    Task<IActionResult> DeleteUser(int id);
}