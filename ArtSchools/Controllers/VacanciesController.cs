using ArtSchools.App.Pagination.Base;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class VacanciesController : ControllerBase
{
    private readonly IRepository<Vacancy, int> _vacancyRepository;

    public VacanciesController(IRepository<Vacancy, int> vacancyRepository)
    {
        _vacancyRepository = vacancyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VacancyDto>>> GetVacancys([FromQuery] BrowseVacancys query)
    {
        
        var entities = await _vacancyRepository.GetAllAsync();

        entities = entities.Where(e => e.SchoolId == query.SchoolId);
        var entityDtos = await entities.PaginateAsync(query);
        return Ok(entityDtos.Map(e=>e.AsDto()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VacancyDto>> GetVacancy(int id)
    {
        var entity = await _vacancyRepository.GetAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity.AsDto());
    }

}