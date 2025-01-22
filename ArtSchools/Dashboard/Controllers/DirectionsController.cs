using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Commands;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Dashboard.Controllers
{
    [Route("dashboard/[controller]")]
    [ApiController]
    [Authorize]
    public class DirectionsController : ControllerBase
    {
        private readonly IRepository<Direction, int> _directionRepository;

        public DirectionsController(IRepository<Direction, int> directionRepository)
        {
            _directionRepository = directionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectionDto>>> GetDirections([FromQuery] BrowseDirections query)
        {
            var entities = await _directionRepository.GetAllAsync();
            entities = entities.Where(e => e.SchoolId == this.User.GetOrgId());
            if (!string.IsNullOrEmpty(query.SearchText))
            {
                query.SearchText = query.SearchText.ToLower();
                entities = entities.Where(e => e.NameEn.ToLower().Contains(query.SearchText) ||
                                    e.NameRu.ToLower().Contains(query.SearchText) ||
                                    e.NameUz.ToLower().Contains(query.SearchText) ||
                                    e.NameOz.ToLower().Contains(query.SearchText)
                );
            }
            var entityDtos = entities.Select(entity => entity.AsDto());
            return Ok(entityDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectionDto>> GetDirection(int id)
        {
            var entity = await _directionRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<DirectionDto>> CreateDirection(UpsertDirection command)
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
            await _directionRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetDirection), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirection(int id, UpsertDirection command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _directionRepository.GetAsync(id);
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
            await _directionRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirection(int id)
        {
            var entity = await _directionRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _directionRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
