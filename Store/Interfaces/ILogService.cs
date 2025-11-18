using Microsoft.AspNetCore.Mvc;

namespace Store.Interfaces;

public interface ILogService
{
    Task<IActionResult> GetLogsAsync([FromHeader]string Authorization);
}