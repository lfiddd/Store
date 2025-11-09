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

    [HttpGet("GetAllUsers")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> GetAllUsers() => await _userService.GetAllUsers(3);
    
    [HttpPost("CreateNewUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> CreateUser(UserQuery newUser, int id_role) => await _userService.CreateNewUserAndLogin(newUser, 3);
    
    [HttpPut("{id}/UpdateUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> UpdateUser(int id, UserQuery updatedUser) => await _userService.UpdateUserAndLogin(id, updatedUser);

    [HttpDelete("{id}/DeleteUser")]
    [RoleAtribute([1, 2])]
    public async Task<IActionResult> DeleteUser(int id) => await _userService.DeleteUser(id);
    
}