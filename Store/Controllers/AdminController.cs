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
    public async Task<IActionResult> GetAllUsers() => await _userService.GetAllUsers(2);
    
    [HttpPost("CreateNewEmployee")]
    [RoleAtribute([1])]
    public async Task<IActionResult> CreateUser(UserQuery newUser, int id_role) => await _userService.CreateNewUserAndLogin(newUser, 2);
    
    [HttpPut("{id}/UpdateEmployee")]
    [RoleAtribute([1])]
    public async Task<IActionResult> UpdateUser(int id, UserQuery updatedUser) => await _userService.UpdateUserAndLogin(id, updatedUser);

    [HttpDelete("{id}/DeleteEmployee")]
    [RoleAtribute([1])]
    public async Task<IActionResult> DeleteUser(int id) => await _userService.DeleteUser(id);
}