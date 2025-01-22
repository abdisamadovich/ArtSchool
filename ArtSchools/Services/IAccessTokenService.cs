namespace ArtSchools.Services;

public interface IAccessTokenService
{
    Task<bool> IsCurrentActiveToken();

    Task DeactivateCurrentAsync();

    Task<bool> IsActiveAsync(string token);

    Task DeactivateAsync(string token);
}