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

[Authorize]
[ApiController]
[Route("dashboard/[controller]")]
public class SchoolsController : ControllerBase
{
    private IRepository<School, int> _schoolRepository;

    public SchoolsController(IRepository<School, int> schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }
    
    [HttpGet("{id}")]
    public async Task<SchoolDto> Get([FromRoute] int id)
    {
        var school =  await _schoolRepository.GetAsync(id);
        if (school == null)
        {
            this.StatusCode(404);
            return null;
        }

        return school.AsDto();
    }

    [HttpGet]
    public async Task<PagedResult<SchoolDto>> Get([FromQuery] BrowseSchools query)
    {
        var entities = (await _schoolRepository.GetAllAsync()).Where(s => !s.IsDeleted);
        
        if (query.RegionId != null && query.RegionId != 0)
            entities = entities.Where(e => e.RegionId == query.RegionId);
        if (query.DistrictId != null && query.DistrictId != 0)
            entities = entities.Where(e => e.DistrictId == query.DistrictId);
        if (!string.IsNullOrEmpty(query.SearchText))
        {
            query.SearchText = query.SearchText.ToLower();
            entities = entities.Where(e => e.NameEn.ToLower().Contains(query.SearchText) ||
                                           e.NameRu.ToLower().Contains(query.SearchText) ||
                                           e.NameUz.ToLower().Contains(query.SearchText) ||
                                           e.NameOz.ToLower().Contains(query.SearchText)
            );
        }

        var schools = await entities.PaginateAsync(query);

        return schools?.Map(u => u.AsDto());
    }

    [HttpPost]
    [AllowPermission("ALL_ACTIONS")]
    public async Task<IActionResult> Post(UpsertSchool command)
    {
        if(command.DomainId == null)
            throw new UIException(new Language(
                $"Maktab domaini kiritish majburiy!",   // Latin Uzbek (Oz)
                $"Мактаб домени киритиш мажбурий!",   // Cyrillic Uzbek (Uz)
                $"Ввод домена школы обязателен!",  // Russian (Ru)
                $"School domain entry is required!" // English (En)
            ), StatusCodes.Status400BadRequest);
        
        if((await _schoolRepository.GetAllAsync()).Any(s=>s.DomainId == command.DomainId))
            throw new UIException(new Language(
                $"Ushbu domain band: {command.DomainId}",   // Latin Uzbek (Oz)
                $"Ушбу домен банд: {command.DomainId}",   // Cyrillic Uzbek (Uz)
                $"Этот домен занят: {command.DomainId}",  // Russian (Ru)
                $"This domain is taken: {command.DomainId}" // English (En)
            ), StatusCodes.Status400BadRequest);
        
        await _schoolRepository.InsertAsync(command.AsEntity());
        return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(UpsertSchool command)
    {
        var school = await _schoolRepository.GetAsync(command.Id);
        
        if (school == null)
        {
            StatusCode(404);
            return null;
        }
        
        if((await _schoolRepository.GetAllAsync()).Any(s=>school.Id != s.Id && s.DomainId == command.DomainId))
            throw new UIException(new Language(
                $"Ushbu domain band: {command.DomainId}",   // Latin Uzbek (Oz)
                $"Ушбу домен банд: {command.DomainId}",   // Cyrillic Uzbek (Uz)
                $"Этот домен занят: {command.DomainId}",  // Russian (Ru)
                $"This domain is taken: {command.DomainId}" // English (En)
            ), StatusCodes.Status400BadRequest);
        
        if (school.Id != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
        {
            throw new UIException(new Language(
                "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                "Доступ запрещен!",    // Russian (Ru)
                "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
        }
        await _schoolRepository.UpdateAsync(command.AsEntity());
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [AllowPermission("ALL_ACTIONS")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var school = await _schoolRepository.GetAsync(id);
        if (school == null)
        {
            StatusCode(404);
            return null;
        }
        
        school.IsDeleted = true;
        await _schoolRepository.UpdateAsync(school);
        
        return Ok();
    }
}