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
}