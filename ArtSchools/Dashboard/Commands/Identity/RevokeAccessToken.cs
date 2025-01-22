namespace ArtSchools.Dashboard.Commands.Identity;

public class RevokeAccessToken
{
    public string AccessToken { get; }

    public RevokeAccessToken(string accessToken)
    {
        AccessToken = accessToken;
    }
}