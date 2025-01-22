namespace ArtSchools.Dashboard.Commands.Identity;

public class UseRefreshToken
{
    public string RefreshToken { get; }

    public UseRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}