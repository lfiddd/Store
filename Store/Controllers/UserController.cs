using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService) => _userService = userService;

    [AllowAnonymous]
    [HttpPost("Authorization")]
    public async Task<IActionResult> Authorization([FromBody] AuthUser authUser) => await _userService.AuthorizationAsync(authUser);

    [HttpGet("GetProfile")]
    public async Task<IActionResult> GetProfile([FromHeader] string userId) => await _userService.GetUserProfile(userId);
    
    [HttpPost("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromHeader] string userId) => await _userService.UpdateUserProfile(userId);
}