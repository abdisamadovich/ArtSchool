using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class DirectionsController : ControllerBase
{
    private readonly IRepository<Direction, int> _directionRepository;
    private readonly IRepository<School, int> _schoolRepository;

    public DirectionsController(IRepository<Direction, int> directionRepository, IRepository<School, int> schoolRepository)
    {
        _directionRepository = directionRepository;
        _schoolRepository = schoolRepository;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<DirectionDto>>> Get([FromQuery] BrowseDirections query)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == query.DomainId);
            
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);

        var entities = await _directionRepository.GetAllAsync();
        entities = entities.Where(e => e.SchoolId == school.Id);
            
        var directions = await entities.PaginateAsync(query);
        return directions?.Map(e => e.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DirectionDto>> Get(int id)
    {
        var entity = await _directionRepository.GetAllAsync();
        var eventEntity = entity.FirstOrDefault(e=>e.Id == id);
        
        if (eventEntity == null)
        {
            return NotFound();
        }
        return Ok(eventEntity.AsDto());
    }
}