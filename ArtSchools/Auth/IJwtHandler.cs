namespace ArtSchools.Auth;

public interface IJwtHandler
{
    JsonWebToken CreateToken(int userId, string role = null, int? schoolId = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);

    JsonWebTokenPayload GetTokenPayload(string accessToken);
}