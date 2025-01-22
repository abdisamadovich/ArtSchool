using ArtSchools.Dashboard.Dtos;

namespace ArtSchools.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly IJwtHandler _jwtHandler;

    public JwtProvider(IJwtHandler jwtHandler)
    {
        _jwtHandler = jwtHandler;
    }

    public AuthDto Create(int userId, string role, int schoolId, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null)
    {
        var jwt = _jwtHandler.CreateToken(userId, role, schoolId, audience, claims);

        return new AuthDto
        {
            AccessToken = jwt.AccessToken,
            Role = jwt.Role,
            Expires = jwt.Expires
        };
    }
}