using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IRepository<Event, int> _eventRepository;
    private readonly IRepository<School, int> _schoolRepository;

    public EventsController(IRepository<Event, int> eventRepository, IRepository<School, int> schoolRepository)
    {
        _eventRepository = eventRepository;
        _schoolRepository = schoolRepository;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<EventDto>>> GetEvents([FromQuery] BrowseEvents query)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == query.DomainId);
            
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);

        var entities = await _eventRepository.GetAllAsync();
        entities = entities.Include(e=>e.Direction).Where(e => e.SchoolId == school.Id);
            
        var events = await entities.PaginateAsync(query);
        return events?.Map(e => e.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(int id)
    {
        var entity = await _eventRepository.GetAllAsync();
        var eventEntity = entity.Include(e=>e.Direction).FirstOrDefault(e=>e.Id == id);


        if (eventEntity == null)
        {
            return NotFound();
        }
        return Ok(eventEntity.AsDto());
    }
}