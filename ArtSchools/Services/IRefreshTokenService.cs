using ArtSchools.Dashboard.Dtos;

namespace ArtSchools.Services;

public interface IRefreshTokenService
{
    Task<string> CreateAsync(int userId);
    Task RevokeAsync(string refreshToken);
    Task<AuthDto> UseAsync(string refreshToken);
}