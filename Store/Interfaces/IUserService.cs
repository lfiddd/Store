using Microsoft.AspNetCore.Mvc;
using Store.Requests;

namespace Store.Interfaces;

public interface IUserService
{
    //Get tasks
    Task<IActionResult> GetAllUsers([FromHeader]string Authorization, int id_role);
    
    //Add new user task
    Task<IActionResult> CreateNewUserAndLogin([FromHeader]string Authorization, UserQuery newUser, int id_role);
    
    //Update user task
    Task<IActionResult> UpdateUserAndLogin([FromHeader]string Authorization, int id, UserQuery updatedUser);
    
    //Delete user task
    Task<IActionResult> DeleteUser([FromHeader]string Authorization, int id);
    
    //Registration task
    Task<IActionResult> RegitrationUser([FromBody]UserQuery registrationUser);
    
    //Authorization Task
    Task<IActionResult> AuthorizationAsync([FromBody] AuthUser authUser);
    
    //Profile tasks
    Task<IActionResult> GetUserProfile([FromHeader]string Authorization);
    Task<IActionResult> UpdateUserProfile([FromHeader]string Authorization, UserQuery updatedProfile);
}