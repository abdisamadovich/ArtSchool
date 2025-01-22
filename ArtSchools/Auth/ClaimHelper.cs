using System.Security.Claims;

namespace ArtSchools.Auth;

public static class ClaimHelper
{
    public static int? GetOrgId(this ClaimsPrincipal user)
    {
        var orgIdString = user.FindAll(m => m.Type == "schoolId")
            ?.Select(m=>m.Value).FirstOrDefault();
            
        if (int.TryParse(orgIdString, out int orgId))
            return orgId;
        
        return null;
    }
    public static bool HasPermission(this ClaimsPrincipal user, string checkPermission)
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