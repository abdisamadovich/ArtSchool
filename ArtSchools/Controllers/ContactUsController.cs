using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.Dashboard.Commands;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class ContactUsController : ControllerBase
{
    private readonly IRepository<School, int> _schoolRepository;
    private readonly IRepository<ContactUs, int> _contactUsRepository;

    public ContactUsController(IRepository<School, int> schoolRepository, IRepository<ContactUs, int> contactUsRepository)
    {
        _schoolRepository = schoolRepository;
        _contactUsRepository = contactUsRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ContactUsDto>> CreateContactUs([FromBody] UpsertContactUs command)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s=>s.DomainId == command.DomainId);
        
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);
        
        var entity = command.AsEntity();
        await _contactUsRepository.InsertAsync(entity);
        
        return Created("", entity);
    }
}