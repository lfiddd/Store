using Microsoft.AspNetCore.Mvc;
using Store.CustomAtributes;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
public class AdminController
{
    private readonly IUserService _userService;
    public AdminController(IUserService userService) => _userService = userService;
    
    [HttpGet("GetAllEmployee")]
    [RoleAtribute([1])]
    public async Task<IActionResult> GetAllEmployee([FromHeader]string Authorization, int id_role) => await _userService.GetAllUsers(Authorization ,2);
    
    [HttpGet("GetAllUsers")]
    [RoleAtribute([1])]
    public async Task<IActionResult> GetAllUsers([FromHeader]string Authorization, int id_role) => await _userService.GetAllUsers(Authorization, 3);
    
    [HttpPost("CreateNewEmployee")]
    [RoleAtribute([1])]
    
    public async Task<IActionResult> CreateEmployee([FromHeader]string Authorization, UserQuery newUser, int id_role ) => await _userService.CreateNewUserAndLogin(Authorization, newUser, 2 );
    
    [HttpPost("CreateNewUser")]
    [RoleAtribute([1])]
    public async Task<IActionResult> CreateUser([FromHeader]string Authorization ,[FromBody]UserQuery newUser, int id_role ) => await _userService.CreateNewUserAndLogin(Authorization, newUser, 3 );
    
    [HttpPut("UpdateEmployeeOrUser/{id}")]
    [RoleAtribute([1])]
    public async Task<IActionResult> UpdateUserOrEmployee([FromHeader]string Authorization, int id, UserQuery updatedUser) => await _userService.UpdateUserAndLogin(Authorization, id, updatedUser);

    [HttpDelete("DeleteUserOrEmployee/{id}")]
    [RoleAtribute([1])]
    public async Task<IActionResult> DeleteUserOrEmployee([FromHeader]string Authorization, int id ) => await _userService.DeleteUser(Authorization, id );
}