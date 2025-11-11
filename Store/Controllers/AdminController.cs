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
    public async Task<IActionResult> GetAllEmployee() => await _userService.GetAllUsers(2);
    
    [HttpGet("GetAllUsers")]
    [RoleAtribute([1])]
    public async Task<IActionResult> GetAllUsers() => await _userService.GetAllUsers(3);
    
    [HttpPost("CreateNewEmployee")]
    [RoleAtribute([1])]
    public async Task<IActionResult> CreateEmployee(UserQuery newUser, int id_role) => await _userService.CreateNewUserAndLogin(newUser, 2);
    
    [HttpPost("CreateNewUser")]
    [RoleAtribute([1])]
    public async Task<IActionResult> CreateUser(UserQuery newUser, int id_role) => await _userService.CreateNewUserAndLogin(newUser, 3);
    
    [HttpPut("UpdateEmployeeOrUser/{id}")]
    [RoleAtribute([1])]
    public async Task<IActionResult> UpdateUserOrEmployee(int id, UserQuery updatedUser) => await _userService.UpdateUserAndLogin(id, updatedUser);

    [HttpDelete("DeleteUserOrEmployee/{id}")]
    [RoleAtribute([1])]
    public async Task<IActionResult> DeleteUserOrEmployee(int id) => await _userService.DeleteUser(id);
}