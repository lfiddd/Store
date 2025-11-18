using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.CustomAtributes;
using Store.Interfaces;
using Store.Requests;

namespace Store.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService) => _userService = userService;

    [HttpPost("Registration")]
    public async Task<IActionResult> RegisterUser([FromBody] UserQuery registrationUser) => await _userService.RegitrationUser(registrationUser);
    [AllowAnonymous]
    [HttpPost("Authorization")]
    public async Task<IActionResult> Authorization([FromBody] AuthUser authUser) => await _userService.AuthorizationAsync(authUser);

    [HttpGet("GetProfile")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> GetProfile([FromHeader] string Authorization) => await _userService.GetUserProfile(Authorization);
    
    [HttpPut("UpdateProfile")]
    [RoleAtribute([1, 2, 3])]
    public async Task<IActionResult> UpdateProfile([FromHeader]string Authorization, UserQuery updatedProfile) => await _userService.UpdateUserProfile(Authorization, updatedProfile);
}