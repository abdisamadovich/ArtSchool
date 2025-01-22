namespace ArtSchools.Dashboard.Commands.Identity;

public class RevokeRefreshToken
{
    public string RefreshToken { get; }

    public RevokeRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}