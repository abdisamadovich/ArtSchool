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

namespace ArtSchools.Dashboard.Controllers
{
    [Route("dashboard/[controller]")]
    [ApiController]
    [Authorize]
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

            var schoolId = this.User.GetOrgId();
            if (schoolId == null)
                return Forbid();

            entities = entities.Where(e => e.SchoolId == schoolId);
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

        [HttpPost]
        public async Task<ActionResult<VacancyDto>> CreateVacancy(UpsertVacancy command)
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
            await _vacancyRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetVacancy), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy(int id, UpsertVacancy command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _vacancyRepository.GetAsync(id);
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
            await _vacancyRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var entity = await _vacancyRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _vacancyRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
