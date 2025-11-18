using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;
using Store.Interfaces;

namespace Store.Services;

public class LogsService : ILogService
{
    private readonly ContextDatabase _context;

    public LogsService(ContextDatabase contextDatabase)
    {
        _context = contextDatabase;
    }
    public async Task<IActionResult> GetLogsAsync(string Authorization)
    {
        var actionLogs = await _context.ActionLogs.ToListAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            data = new {actionLogs = actionLogs}
        });
    }
}