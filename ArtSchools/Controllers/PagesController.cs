using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class PagesController : ControllerBase
{
    private readonly IRepository<Page, int> _pageRepository;
    private readonly IRepository<School, int> _schoolRepository;

    public PagesController(IRepository<Page, int> pageRepository, IRepository<School, int> schoolRepository)
    {
        _pageRepository = pageRepository;
        _schoolRepository = schoolRepository;
    }

    [HttpGet("/getbyurl")]
    public async Task<ActionResult<PageDto>> Get([FromQuery] string url, string domainId)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == domainId);
            
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);
        
        var entity = (await _pageRepository.GetAllAsync()).FirstOrDefault(s => s.SchoolId == school.Id && s.Url == url);
        var dto = entity.AsDto();
        
        return dto;
    }
}