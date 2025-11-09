using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IUserService
{
    //Get tasks
    Task<IActionResult> GetAllUsers(int id_role);
    
    //Add new user task
    Task<IActionResult> CreateNewUserAndLogin(UserQuery newUser, int id_role);
    
    //Update user task
    Task<IActionResult> UpdateUserAndLogin(int id, UserQuery updatedUser);
    
    //Delete user task
    Task<IActionResult> DeleteUser(int id);
    
    //Authorization Task
    Task<IActionResult> AuthorizationAsync([FromBody] AuthUser authUser);
    
    //Profile tasks
    Task<IActionResult> GetUserProfile([FromHeader] string userId);
    Task<IActionResult> UpdateUserProfile([FromHeader] string userId);
}