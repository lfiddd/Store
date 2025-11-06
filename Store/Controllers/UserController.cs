using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;

namespace Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService) => _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetAll() => await _userService.GetAllUsers();
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id) => await _userService.GetUserById(id);
    
    [HttpGet("Search by name")]
    public async Task<IActionResult> GetUserByName([FromQuery] string? fullName) => await _userService.GetUserByName(fullName);
}