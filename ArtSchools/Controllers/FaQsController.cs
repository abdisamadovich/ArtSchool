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
public class FaQsController : ControllerBase
{
    private readonly IRepository<FAQ, int> _faqRepository;
    private readonly IRepository<School, int> _schoolRepository;

    public FaQsController(IRepository<FAQ, int> faqRepository, IRepository<School, int> schoolRepository)
    {
        _faqRepository = faqRepository;
        _schoolRepository = schoolRepository;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<FAQDto>>> GetFAQs([FromQuery] BrowseFAQs query)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == query.DomainId);
            
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);

        var entities = await _faqRepository.GetAllAsync();
        entities = entities.Where(e => e.SchoolId == school.Id);
            
        var faqs = await entities.PaginateAsync(query);
        return faqs?.Map(e => e.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FAQDto>> GetFAQ(int id)
    {
        var entity = await _faqRepository.GetAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity.AsDto());
    }
}