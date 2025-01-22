using System.Net;
using System.Text.RegularExpressions;
using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Commands;
using ArtSchools.Dashboard.Commands.Identity;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Services;

public class IdentityService : IIdentityService
{
    private readonly IRepository<User, int> _userRepository;
    private readonly IRepository<Role, int> _roleRepository;
    private readonly IRepository<School, int> _schoolRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogger<IdentityService> _logger;

    public IdentityService(IRepository<User, int> userRepository, IPasswordService passwordService,
        IJwtProvider jwtProvider, IRefreshTokenService refreshTokenService, ILogger<IdentityService> logger, IRepository<Role, int> roleRepository, IRepository<School, int> schoolRepository)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtProvider = jwtProvider;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
        _roleRepository = roleRepository;
        _schoolRepository = schoolRepository;
    }

    public async Task<UserDto> GetAsync(int id)
    {
        var user = (await _userRepository.GetAllAsync())
            .Where(u => u.Id == id)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefault();
        
        return user.AsDto();
    }

    public Task<User> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthDto> SignInAsync(SignIn command)
    {
        var user = await (await _userRepository.GetAsync(u => u.Login.Equals(command.Login)))
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync();
        
        if (user is null || !_passwordService.IsValid(user.Password, command.Password) || user.IsDeleted)
        {
            _logger.LogError($"User with email: {command.Login} was not found.");
            throw new UIException(new Language(
                "Login yoki parol noto'g'ri kiritildi!",   // Latin Uzbek (Oz)
                "Логин ёки парол нотўғри киритилди!",     // Cyrillic Uzbek (Uz)
                "Логин или пароль введены неверно!",      // Russian (Ru)
                "Incorrect login or password entered!"    // English (En)
            ), StatusCodes.Status401Unauthorized);

        }
        
        var school = await _schoolRepository.GetAsync(user.SchoolId);
        
        if(school.DomainId != command.DomainId)
            throw new UIException(new Language(
                "Login yoki parol noto'g'ri kiritildi!",   // Latin Uzbek (Oz)
                "Логин ёки парол нотўғри киритилди!",     // Cyrillic Uzbek (Uz)
                "Логин или пароль введены неверно!",      // Russian (Ru)
                "Incorrect login or password entered!"    // English (En)
            ), StatusCodes.Status401Unauthorized);
        
        var permissions = user?.UserRoles
            .SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.PermissionName))
            .Distinct();
        
        var claims = permissions != null
            ? new Dictionary<string, IEnumerable<string>>
            { 
                ["permissions"] = permissions
            }
            : null;
        
        var auth = _jwtProvider.Create(user.Id, user.UserRoles.FirstOrDefault().Role.Name, user.SchoolId, claims:claims);
        auth.RefreshToken = await _refreshTokenService.CreateAsync(user.Id);

        _logger.LogInformation($"User with id: {user.Id} has been authenticated.");

        return auth;
    }

    public async Task SignUpAsync(UpsertUser command)
    {
        var user = (await _userRepository.GetAsync(u=>u.Login.Equals(command.Login))).FirstOrDefault();
        if (user is {})
        {
            _logger.LogError($"Login already in use: {command.Login}");
            throw new UIException(new Language(
                $"Login allaqachono mavjud: {command.Login}", 
                $"Логин аллақачон мавжуд: {command.Login}", 
                $"Логин уже используется: {command.Login}", 
                $"Login already in use: {command.Login}"
                ),StatusCodes.Status409Conflict);
        }

        var password = _passwordService.Hash(command.Password);
        user = new User()
        {
            Login = command.Login,
            Password = password,
            FirstName = command.FirstName,
            LastName = command.LastName,
            MiddleName = command.MiddleName,
            PhoneNumber = command.PhoneNumber,
            UserRoles = new List<UserRole> {new UserRole() {RoleId = command.Role.Id}},
            SchoolId = command.SchoolId,
            CreatedAt = DateTime.Now
        };
        await _userRepository.InsertAsync(user);
        
        _logger.LogInformation($"Created an account for the user with id: {user.Id}.");
    }

    public async Task InsertAdmin()
    {
        var login = "admin";
        var pass = "Art@dm1n";
        var user = (await _userRepository.GetAsync(u=>u.Login.Equals("admin"))).FirstOrDefault();
        
        if (user != null)
            return;

        var password = _passwordService.Hash(pass);
        var role = new Role
        {
            Name = "Admin",
            RolePermissions = new RolePermission[]
            {
                new RolePermission()
                {
                    Permission = new Permission()
                    {
                        PermissionName = "ALL_ACTIONS"
                    }
                }
            }
        };
        await _roleRepository.InsertAsync(role);
        user = new User()
        {
            Login = login,
            Password = password,
            FirstName = "",
            LastName = "",
            MiddleName = "",
            PhoneNumber = "",
            UserRoles = new List<UserRole>
            {
                new UserRole() {
                    RoleId = role.Id
                }
            },
            SchoolId = 1
        };
        
        await _userRepository.InsertAsync(user);
    }
}