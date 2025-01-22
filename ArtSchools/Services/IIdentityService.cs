using ArtSchools.Dashboard.Commands.Identity;
using ArtSchools.Dashboard.Dtos;

namespace ArtSchools.Services;

public interface IIdentityService
{
    Task<UserDto> GetAsync(int id);
    Task<AuthDto> SignInAsync(SignIn command);
    Task SignUpAsync(UpsertUser command);
    Task InsertAdmin();
}