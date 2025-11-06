using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Store.DatabaseContext;

namespace Store.CustomAtributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleAtribute : Attribute, IAsyncActionFilter
{
    private int[] id_roles;

    public RoleAtribute(int[] _id_roles)
    {
        id_roles = _id_roles;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ContextDatabase>();
        string? token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new JsonResult(new { error = "Session don't transfer" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        var session = await dbContext.Sessions.Include(x => x.User)
            .FirstOrDefaultAsync(session => session.token == token);

        if (session == null)
        {
            context.Result = new JsonResult(new { error = "Session not found" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            
            return;
        }

        if (!id_roles.Contains(session.User.id_role))
        {
            context.Result = new JsonResult(new { error = "Haven't permissions" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            
            return;
        }

        await next();
    }
}