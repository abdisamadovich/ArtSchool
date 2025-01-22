using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArtSchools.Auth;

public class AllowPermissionFilter: IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var permissionCheckAttributes = context.ActionDescriptor.EndpointMetadata
            .OfType<AllowPermissionAttribute>();

        foreach (var attribute in permissionCheckAttributes)
        {
            var hasPermission = attribute.Permissions.All(permission => 
                CheckUserPermission(context.HttpContext.User, permission));

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
                return;
            }
        }

        await next();
    }

    private bool CheckUserPermission(ClaimsPrincipal user, string checkPermission)
    {
        var userPermissions = user.FindAll(c => c.Type == "permissions");
        foreach (var userPermission in userPermissions)
        {
            if (userPermission?.Value == checkPermission)
                return true;
        }
        
        return false;
    }
}