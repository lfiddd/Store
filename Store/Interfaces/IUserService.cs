using Microsoft.AspNetCore.Mvc;

namespace Store.Interfaces;

public interface IUserService
{
    //Get tasks
    Task<IActionResult> GetAllUsers();
    Task<IActionResult> GetUserById(string id);
}