using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ArtSchools;

public class AuthOptions
{
    public const string ISSUER = "ArtSchoolAuthServer";
    public const string AUDIENCE = "ArtSchoolAuthClient";
    const string KEY = "artschoolsecret_secretsecretsecretkey129!";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}