using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService) => _userService = userService;
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => await _userService.GetAllUsers();
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id) => await _userService.GetUserById(id);
    
    [HttpGet("Search by name")]
    public async Task<IActionResult> GetUserByName([FromQuery] string? fullName) => await _userService.GetUserByName(fullName);
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserQuery newUser) => await _userService.CreateNewUserAndLogin(newUser);
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserQuery updatedUser) => await _userService.UpdateUserAndLogin(id, updatedUser);

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) => await _userService.DeleteUser(id);
}