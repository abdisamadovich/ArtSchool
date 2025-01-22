using ArtSchools.App.Pagination.Base;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Controllers;

[ApiController]
[Route("[controller]")]
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
    [HttpGet("domain/{domainId}")]
    public async Task<ActionResult<SchoolDto>> GetByDomain([FromRoute] string domainId)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == domainId);
        
        if (school == null)
        {
            return Ok();
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

}