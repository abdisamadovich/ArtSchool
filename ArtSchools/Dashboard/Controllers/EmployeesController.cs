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

    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee, int> _employeeRepository;

        public EmployeesController(IRepository<Employee, int> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EmployeeDto>>> GetEmployees([FromQuery] BrowseEmployees query)
        {
            var schoolId = this.User.GetOrgId();
            if (schoolId == null)
                return Forbid();
            
            var entities = await _employeeRepository.GetAllAsync();
            entities = entities.Where(e => e.SchoolId == schoolId);
            
            var employe = await entities.PaginateAsync(query);
            
            return employe.Map(e => e.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var entity = await _employeeRepository.GetAllAsync();
            var employe = entity.FirstOrDefault(e=>e.Id == id);
            
            if (employe == null)
                return NotFound();
            
            return Ok(employe.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(UpsertEmployee command)
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
            await _employeeRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetEmployee), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpsertEmployee command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _employeeRepository.GetAsync(id);
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
            await _employeeRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var entity = await _employeeRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
