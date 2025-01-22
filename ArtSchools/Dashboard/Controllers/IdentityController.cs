using System.Security.Claims;
using ArtSchools.Dashboard.Commands.Identity;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ArtSchools.Dashboard.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    public IdentityController(IIdentityService identityService, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService)
    {
        _identityService = identityService;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        
        if (!int.TryParse(userIdClaim, out int userId))
        {
            return BadRequest();
        }
        
        var user =  await _identityService.GetAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<AuthDto> SignIn([FromBody] SignIn command)
    {
        var token = await _identityService.SignInAsync(command);
        return token;
    }

    [HttpPost("access-tokens/revoke")]
    public async Task RevokeAccessToken(RevokeAccessToken command)
    {
        await _accessTokenService.DeactivateAsync(command.AccessToken);
        StatusCode(204);
    }
    
    [HttpPost("refresh-tokens/use")]
    public async Task<AuthDto> UseRefreshToken(UseRefreshToken command)
    {
        return await _refreshTokenService.UseAsync(command.RefreshToken);
    }
    
    [HttpPost("refresh-tokens/revoke")]
    public async Task RevokeRefreshToken(RevokeRefreshToken command)
    {
        await _refreshTokenService.RevokeAsync(command.RefreshToken);
        StatusCode(204);
    }
    
}