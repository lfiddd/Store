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
    public async Task<IActionResult> GetAllUsers([FromHeader]string Authorization) => await _userService.GetAllUsers(Authorization, 3);
    
    [HttpPost("CreateUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> CreateUser([FromHeader]string Authorization ,UserQuery newUser, int id_role ) => await _userService.CreateNewUserAndLogin(Authorization ,newUser, 3 );
    
    [HttpPut("{id}/UpdateUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateUser([FromHeader]string Authorization, int id, UserQuery updatedUser) => await _userService.UpdateUserAndLogin(Authorization, id, updatedUser);

    [HttpDelete("{id}/DeleteUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteUser([FromHeader]string Authorization, int id) => await _userService.DeleteUser(Authorization ,id );
    
}