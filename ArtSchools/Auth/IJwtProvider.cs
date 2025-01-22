using ArtSchools.Dashboard.Dtos;

namespace ArtSchools.Auth;

public interface IJwtProvider
{
    AuthDto Create(int userId, string role, int schoolId, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);
}