using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store.Requests;
using Store.CustomAtributes;

namespace Store.Controllers;

[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IUserService _userService;
    public EmployeeController(IUserService userService) => _userService = userService;

    [HttpGet("GetUsers")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> GetAllUsers([FromHeader]string Authorization) => await _userService.GetAllUsers(3, Authorization);
    
    [HttpPost("CreateUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> CreateUser(UserQuery newUser, int id_role ,[FromHeader]string Authorization) => await _userService.CreateNewUserAndLogin(newUser, 3 ,Authorization);
    
    [HttpPut("{id}/UpdateUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateUser(int id, UserQuery updatedUser ,[FromHeader]string Authorization) => await _userService.UpdateUserAndLogin(id, updatedUser, Authorization);

    [HttpDelete("{id}/DeleteUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteUser(int id, [FromHeader]string Authorization) => await _userService.DeleteUser(id ,Authorization);
    
}