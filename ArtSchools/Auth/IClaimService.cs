namespace ArtSchools.Auth;

public interface IClaimService
{
    int? GetOrganizationId();
}

public class ClaimService : IClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? GetOrganizationId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return null;

        var orgIdString = user.FindAll(m => m.Type.ToLower() == "orgid")
            ?.Select(m=>m.Value).FirstOrDefault();
            
        if (int.TryParse(orgIdString, out int orgId))
            return orgId;
        
        return null;
    }
}