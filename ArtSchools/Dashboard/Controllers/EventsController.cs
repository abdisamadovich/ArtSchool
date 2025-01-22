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
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Dashboard.Controllers
{
    [Route("dashboard/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<Event, int> _eventRepository;

        public EventsController(IRepository<Event, int> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EventDto>>> GetEvents([FromQuery] BrowseEvents query)
        {
            var schoolId = this.User.GetOrgId();
            if (schoolId == null)
                return Forbid();
            
            var entities = await _eventRepository.GetAllAsync();
            entities = entities.Include(e=>e.Direction).Where(e => e.SchoolId == schoolId);
            
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

        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(UpsertEvent command)
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
            command.CreatedAt = DateTime.Now;
            var entity = command.AsEntity();
            await _eventRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetEvent), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, UpsertEvent command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _eventRepository.GetAsync(id);
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
            await _eventRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var entity = await _eventRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _eventRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
