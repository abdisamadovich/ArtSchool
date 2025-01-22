using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Auth;
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
public class ContactUsController : ControllerBase
{
    private readonly IRepository<ContactUs, int> _contactUsRepository;

    public ContactUsController(IRepository<ContactUs, int> contactUsRepository)
    {
        _contactUsRepository = contactUsRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactUsDto>> Get(int id)
    {
        var entity = await _contactUsRepository.GetAsync(id);
        if (entity is null)
            return NotFound();
        
        var dto = entity.AsDto();
        
        entity.IsNew = false;
        await _contactUsRepository.UpdateAsync(entity);
        
        return dto;
    }
    
    [HttpGet]
    public async Task<ActionResult<ContactUsDto>> Get([FromQuery] BrowseContactUs query)
    {
        var entities = await _contactUsRepository.GetAllAsync();
        
        var schoolId = this.User.GetOrgId();
        if (schoolId == null)
            return Forbid();
            
        entities = entities.Where(e => e.SchoolId == schoolId);
            
        if (!string.IsNullOrEmpty(query.SearchText))
        {
            query.SearchText = query.SearchText.ToLower();
            entities = entities.Where(e => e.Firstname.ToLower().Contains(query.SearchText) ||
                                           e.Lastname.ToLower().Contains(query.SearchText) ||
                                           e.PhoneNumber.ToLower().Contains(query.SearchText) ||
                                           e.Email.ToLower().Contains(query.SearchText)
            );
        }
            
        var entityDtos = await entities.PaginateAsync(query);
            
        return Ok(entityDtos.Map(e=>e.AsDto()));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _contactUsRepository.GetAsync(id);
        if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
        {
            throw new UIException(new Language(
                "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                "Доступ запрещен!",    // Russian (Ru)
                "Access denied!"       // English (En)),
            ), StatusCodes.Status403Forbidden);
        }
        await _contactUsRepository.DeleteAsync(id);
        return NoContent();
    }
}