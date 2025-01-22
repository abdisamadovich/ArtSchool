using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;

namespace ArtSchools.Entities;

[Table("refresh_tokens", Schema = "identity")]
public class RefreshToken : IIdentifiable<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public bool Revoked => RevokedAt.HasValue;

    protected RefreshToken()
    {
    }

    public RefreshToken(int userId, string token, DateTime createdAt,
        DateTime? revokedAt = null)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            var emptyRefreshTokenMessage = new Language(
                "Bo'sh yangilash tokeni",       // Latin Uzbek (Oz)
                "Бўш янгилаш токени",           // Cyrillic Uzbek (Uz)
                "Пустой токен обновления",      // Russian (Ru)
                "Empty refresh token"           // English (En)
            );
            throw new UIException(emptyRefreshTokenMessage, StatusCodes.Status400BadRequest);

        }
        
        UserId = userId;
        Token = token;
        CreatedAt = createdAt;
        RevokedAt = revokedAt;
    }

    public void Revoke(DateTime revokedAt)
    {
        if (Revoked)
        {
            var languageMessage = new Language(
                "Bekor qilingan yangilash tokeni",   // Latin Uzbek (Oz)
                "Бекор қилинган янгилаш токени",      // Cyrillic Uzbek (Uz)
                "Отозванный токен обновления",       // Russian (Ru)
                "Revoked refresh token"              // English (En)
            );
            throw new UIException(languageMessage, StatusCodes.Status401Unauthorized);

        }

        RevokedAt = revokedAt;
    }
}