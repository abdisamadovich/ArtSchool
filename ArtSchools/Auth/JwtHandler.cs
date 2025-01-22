using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ArtSchools.Auth;

internal sealed class JwtHandler : IJwtHandler
{
    private static readonly IDictionary<string, IEnumerable<string>> EmptyClaims =
        new Dictionary<string, IEnumerable<string>>();

    private static readonly ISet<string> DefaultClaims = new HashSet<string>
    {
        JwtRegisteredClaimNames.Sub,
        JwtRegisteredClaimNames.UniqueName,
        JwtRegisteredClaimNames.Jti,
        JwtRegisteredClaimNames.Iat,
        ClaimTypes.Role,
    };

    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly JwtOptions _options;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly SigningCredentials _signingCredentials;
    private readonly string _issuer;

    public JwtHandler(JwtOptions options, TokenValidationParameters tokenValidationParameters)
    {
        var issuerSigningKey = tokenValidationParameters.IssuerSigningKey;
        if (issuerSigningKey is null)
        {
            throw new InvalidOperationException("Issuer signing key not set.");
        }

        if (string.IsNullOrWhiteSpace(options.Algorithm))
        {
            throw new InvalidOperationException("Security algorithm not set.");
        }

        _options = options;
        _tokenValidationParameters = tokenValidationParameters;
        _signingCredentials = new SigningCredentials(issuerSigningKey, _options.Algorithm);
        _issuer = options.Issuer;
    }

    public JsonWebToken CreateToken(int userId, string role = null, int? schoolId = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null)
    {
        var now = DateTime.UtcNow;
        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
        };
        if (!string.IsNullOrWhiteSpace(role))
        {
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (!string.IsNullOrWhiteSpace(audience))
        {
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
        }

        if (schoolId != null)
        {
            jwtClaims.Add(new Claim("schoolId", schoolId.ToString()));
        }
        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var (claim, values) in claims)
            {
                customClaims.AddRange(values.Select(value => new Claim(claim, value)));
            }

            jwtClaims.AddRange(customClaims);
        }

        var expires = _options.Expiry.HasValue
            ? now.AddMilliseconds(_options.Expiry.Value.TotalMilliseconds)
            : now.AddMinutes(_options.ExpiryMinutes);

        var jwt = new JwtSecurityToken(
            _issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            RefreshToken = string.Empty,
            Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
            Id = userId.ToString(),
            Role = role ?? string.Empty,
            Claims = claims ?? EmptyClaims
        };
    }

    public JsonWebTokenPayload GetTokenPayload(string accessToken)
    {
        _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
            out var validatedSecurityToken);
        if (!(validatedSecurityToken is JwtSecurityToken jwt))
        {
            return null;
        }

        return new JsonWebTokenPayload
        {
            Subject = jwt.Subject,
            Role = jwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
            Expires = new DateTimeOffset(jwt.ValidTo).ToUnixTimeSeconds(),
            Claims = jwt.Claims.Where(x => !DefaultClaims.Contains(x.Type))
                .GroupBy(c => c.Type)
                .ToDictionary(k => k.Key, v => v.Select(c => c.Value))
        };
    }
}