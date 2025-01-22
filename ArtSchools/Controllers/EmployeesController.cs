using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
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
public class EmployeesController : ControllerBase
{
    private readonly IRepository<Employee, int> _employeeRepository;
    private readonly IRepository<School, int> _schoolRepository;

    public EmployeesController(IRepository<Employee, int> employeeRepository, IRepository<School, int> schoolRepository)
    {
        _employeeRepository = employeeRepository;
        _schoolRepository = schoolRepository;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<EmployeeDto>>> GetEmployees([FromQuery] BrowseEmployees query)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == query.DomainId);
            
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);

        var entities = await _employeeRepository.GetAllAsync();
        entities = entities.Where(e => e.SchoolId == school.Id);
            
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
}