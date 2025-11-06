using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IUserService
{
    //Get tasks
    Task<IActionResult> GetAllUsers();
    Task<IActionResult> GetUserById(int id);
    Task<IActionResult> GetUserByName(string fullName);
    
    //Add new user task
    Task<IActionResult> CreateNewUserAndLogin(UserQuery newUser);
    
    //Update user task
    Task<IActionResult> UpdateUserAndLogin(int id, UserQuery updatedUser);
}