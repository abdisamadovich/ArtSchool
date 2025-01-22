using ArtSchools.App.Pagination.Base;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Commands;
using ArtSchools.Dashboard.Commands.Identity;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Entities.Context;
using ArtSchools.Repositories.Base;
using ArtSchools.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Dashboard.Controllers;

[Authorize]
[ApiController]
[Route("dashboard/[controller]")]
[AllowPermission("ALL_ACTIONS")]
public class UsersController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IRepository<User, int> _userRepository;
    private readonly DbContext _applicationDbContext;
    private readonly IPasswordService _passwordService;

    public UsersController(IIdentityService identityService, IRepository<User, int> userRepository, IApplicationDbContext applicationDbContext, IPasswordService passwordService)
    {
        _identityService = identityService;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _applicationDbContext = applicationDbContext.Context;
    }

    [HttpGet("{id}")]
    public async Task<UserDto> Get([FromRoute] int id)
    {
        var user =  (await _userRepository.GetAllAsync())
            .Include(u => u.School)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r=>r.RolePermissions)
            .ThenInclude(rp=>rp.Permission)
            .FirstOrDefault(u => u.Id == id && !u.IsDeleted);
        if (user == null)
        {
            this.StatusCode(404);
            return null;
        }

        return user.AsDto();
    }

    [HttpGet]
    public async Task<PagedResult<UserDto>> Get([FromQuery] BrowseUsers query)
    {
        var users = await (await _userRepository.GetAllAsync())
            .Include(u => u.School)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r=>r.RolePermissions)
            .ThenInclude(rp=>rp.Permission)
            .Where(u=> !u.IsDeleted)
            .PaginateAsync(query);

        return users?.Map(u => u.AsDto());
    }

    [HttpPost]
    public async Task<IActionResult> Post(UpsertUser command)
    {
        await _identityService.SignUpAsync(command);
        return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(UpsertUser command)
    {
        var user = (await _userRepository.GetAllAsync())
            .Include(u => u.UserRoles)
            .FirstOrDefault(u=>u.Id == command.Id);
        
        if (user == null)
        {
            StatusCode(404);
            return null;
        }
        
        user.FirstName = command.FirstName;
        user.LastName = command.LastName;
        user.MiddleName = command.MiddleName;
        user.PhoneNumber = command.PhoneNumber;
        user.SchoolId = command.SchoolId;

        if (!string.IsNullOrEmpty(command.Password))
        {
            user.Password = _passwordService.Hash(command.Password);
        }
        
        if(user.UserRoles.FirstOrDefault().RoleId != command.Role.Id)
        { 
            _applicationDbContext.Set<UserRole>().RemoveRange(user.UserRoles);
            user.UserRoles = new List<UserRole>()
            {
                new UserRole()
                {
                    UserId = user.Id,
                    RoleId = command.Role.Id
                }
            };
        } 
        
        await _userRepository.UpdateAsync(user);
        
        return Ok();
    }
    
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePass(UpsertUser command)
    {
        var user = (await _userRepository.GetAllAsync())
            .FirstOrDefault(u=>u.Id == command.Id);
        
        if (user == null)
        {
            StatusCode(404);
            return null;
        }
        user.Password = _passwordService.Hash(command.Password);
        
        await _userRepository.UpdateAsync(user);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null)
        {
            StatusCode(404);
            return null;
        }
        
        user.IsDeleted = true;
        await _userRepository.UpdateAsync(user);
        
        return Ok();
    }
}