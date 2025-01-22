using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Commands;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Dashboard.Controllers;

[Route("dashboard/[controller]")]
[ApiController]
[Authorize]
public class PagesController : ControllerBase
{
    private readonly IRepository<Page, int> _pageRepository;

    public PagesController(IRepository<Page, int> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PageDto>> Get(int id)
    {
        var entity = await _pageRepository.GetAsync(id);
        if (entity is null)
            return NotFound();
        
        var dto = entity.AsDto();
        
        return dto;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PageDto>>> Browse([FromQuery] BrowsePages query)
    {
        var entities = await _pageRepository.GetAllAsync();
        
        var schoolId = this.User.GetOrgId();
        if (schoolId == null)
            return Forbid();
        
        entities = entities.Where(e => e.SchoolId == schoolId);

        var entityDtos = await entities.PaginateAsync(query);
        
        return Ok(entityDtos.Map(e=>e.AsDto()));
    }

    [HttpPost]
    public async Task<ActionResult<UpsertPage>> Create(UpsertPage command)
    {
        if (command.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
        {
            throw new UIException(new Language(
                "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                "Доступ запрещен!",    // Russian (Ru)
                "Access denied!"       // English (En)),
            ), StatusCodes.Status403Forbidden);
        }
        var entity = command.AsEntity();
        
        await _pageRepository.InsertAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity.AsDto());
    }
   

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpsertPage command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var entity = await _pageRepository.GetAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
        {
            throw new UIException(new Language(
                "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                "Доступ запрещен!",    // Russian (Ru)
                "Access denied!"       // English (En)),
            ), StatusCodes.Status403Forbidden);
        }
        entity = command.AsEntity();

        await _pageRepository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _pageRepository.GetAsync(id);
        if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
        {
            throw new UIException(new Language(
                "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                "Доступ запрещен!",    // Russian (Ru)
                "Access denied!"       // English (En)),
            ), StatusCodes.Status403Forbidden);
        }
        await _pageRepository.DeleteAsync(id);
        return NoContent();
    }
}