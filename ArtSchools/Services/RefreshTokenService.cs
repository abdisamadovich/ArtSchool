using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Services;

public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRepository<RefreshToken, int> _refreshTokenRepository;
        private readonly IRepository<User, int> _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRng _rng;

        public RefreshTokenService(IRepository<RefreshToken, int> refreshTokenRepository,
            IRepository<User, int> userRepository, IJwtProvider jwtProvider, IRng rng)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _rng = rng;
        }

        public async Task<string> CreateAsync(int userId)
        {
            var token = _rng.Generate(30, true);
            var refreshToken = new RefreshToken( userId, token, DateTime.UtcNow);

            await _refreshTokenRepository.InsertAsync(refreshToken);

            return token;
        }

        public async Task RevokeAsync(string refreshToken)
        {
            var token = await (await _refreshTokenRepository.GetAsync(r=>r.Token == refreshToken)).SingleOrDefaultAsync();
            if (token is null)
            {
                var languageMessage = new Language(
                    "Noto'g'ri yangilash tokeni",   // Latin Uzbek (Oz)
                    "Нотўғри янгилаш токени",       // Cyrillic Uzbek (Uz)
                    "Неверный токен обновления",    // Russian (Ru)
                    "Invalid refresh token"         // English (En)
                );
                throw new UIException(languageMessage, StatusCodes.Status401Unauthorized);

            }

            token.Revoke(DateTime.UtcNow);
            await _refreshTokenRepository.UpdateAsync(token);
        }

        public async Task<AuthDto> UseAsync(string refreshToken)
        {
            var token = await (await _refreshTokenRepository.GetAsync(r=>r.Token == refreshToken)).SingleOrDefaultAsync();

            if (token is null)
            {
                var languageMessage = new Language(
                    "Noto'g'ri yangilash tokeni",   // Latin Uzbek (Oz)
                    "Нотўғри янгилаш токени",       // Cyrillic Uzbek (Uz)
                    "Неверный токен обновления",    // Russian (Ru)
                    "Invalid refresh token"         // English (En)
                );
                throw new UIException(languageMessage, StatusCodes.Status401Unauthorized);
            }

            if (token.Revoked)
            {
                var languageMessage = new Language(
                    "Bekor qilingan yangilash tokeni",   // Latin Uzbek (Oz)
                    "Бекор қилинган янгилаш токени",      // Cyrillic Uzbek (Uz)
                    "Отозванный токен обновления",       // Russian (Ru)
                    "Revoked refresh token"              // English (En)
                );
                throw new UIException(languageMessage, StatusCodes.Status401Unauthorized);
            }

            var user = await _userRepository.GetAsync(token.UserId);
            if (user is null)
            {
                throw new UIException(new Language(
                    $"Foydalanuvchi topilmadi: {token.UserId}",   // Latin Uzbek (Oz)
                    $"Фойдаланувчи топилмади: {token.UserId}",   // Cyrillic Uzbek (Uz)
                    $"Пользователь не найден: {token.UserId}",  // Russian (Ru)
                    $"User with login: {token.UserId} was not found." // English (En)
                ), StatusCodes.Status404NotFound);
            }
            
            var permissions = user?.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.PermissionName))
                .Distinct();

            var claims = permissions != null
                ? new Dictionary<string, IEnumerable<string>>
                {
                    ["permissions"] = permissions
                }
                : null;
            var auth = _jwtProvider.Create(token.UserId, user.UserRoles.FirstOrDefault().Role.Name, schoolId: user.SchoolId, claims: claims);
            auth.RefreshToken = refreshToken;

            return auth;
        }
    }