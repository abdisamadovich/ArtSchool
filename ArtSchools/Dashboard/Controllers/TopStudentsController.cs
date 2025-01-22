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
    public class TopStudentsController : ControllerBase
    {
        private readonly IRepository<TopStudent, int> _topstudentRepository;

        public TopStudentsController(IRepository<TopStudent, int> topstudentRepository)
        {
            _topstudentRepository = topstudentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopStudentDto>>> GetTopStudents([FromQuery] BrowseTopStudents query)
        {
            var schoolId = this.User.GetOrgId();
            if (schoolId == null)
                return Forbid();
            
            var entities = await _topstudentRepository.GetAllAsync();
            entities = entities.Where(e => e.SchoolId == schoolId);
            var entityDtos = await entities.PaginateAsync(query);
            return Ok(entityDtos.Map(e=>e.AsDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopStudentDto>> GetTopStudent(int id)
        {
            var entity = await _topstudentRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<TopStudentDto>> CreateTopStudent(UpsertTopStudent command)
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
            await _topstudentRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetTopStudent), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopStudent(int id, UpsertTopStudent command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _topstudentRepository.GetAsync(id);
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
            await _topstudentRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopStudent(int id)
        {
            var entity = await _topstudentRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _topstudentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
