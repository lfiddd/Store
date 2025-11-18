using Microsoft.AspNetCore.Mvc;
using Store.CustomAtributes;
using Store.Interfaces;

namespace Store.Controllers;

[ApiController]
public class LogsController : ControllerBase
{
    private readonly ILogService _logService;
    public LogsController(ILogService logService) => _logService = logService;

    [HttpGet("GetLogs")] 
    [RoleAtribute([1])]
    public async Task<IActionResult> GetLogsUsers([FromHeader] string Authorization) => await _logService.GetLogsAsync(Authorization);
}