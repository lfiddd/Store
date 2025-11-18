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
    
    public async Task<IActionResult> CreateEmployee(UserQuery newUser, int id_role ,[FromHeader]string Authorization) => await _userService.CreateNewUserAndLogin(newUser, 2 ,Authorization);
    
    [HttpPost("CreateNewUser")]
    [RoleAtribute([1])]
    public async Task<IActionResult> CreateUser([FromHeader]string Authorization ,[FromBody]UserQuery newUser, int id_role ) => await _userService.CreateNewUserAndLogin(Authorization, newUser, 3 );
    
    [HttpPut("UpdateEmployeeOrUser/{id}")]
    [RoleAtribute([1])]
    public async Task<IActionResult> UpdateUserOrEmployee(int id, UserQuery updatedUser ,[FromHeader]string Authorization) => await _userService.UpdateUserAndLogin(id, updatedUser ,Authorization);

    [HttpDelete("DeleteUserOrEmployee/{id}")]
    [RoleAtribute([1])]
    public async Task<IActionResult> DeleteUserOrEmployee(int id ,[FromHeader]string Authorization) => await _userService.DeleteUser(id ,Authorization);
}