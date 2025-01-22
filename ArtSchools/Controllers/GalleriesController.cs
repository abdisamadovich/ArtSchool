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
using File = System.IO.File;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class GalleriesController : ControllerBase
{
    private readonly IRepository<Gallery, int> _galleryRepository;
    private readonly IRepository<Entities.File, int> _fileRepository;
    private readonly IRepository<School, int> _schoolRepository;

    public GalleriesController(IRepository<Gallery, int> galleryRepository, IRepository<Entities.File, int> fileRepository, IRepository<School, int> schoolRepository)
    {
        _galleryRepository = galleryRepository;
        _fileRepository = fileRepository;
        _schoolRepository = schoolRepository;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<GalleryDto>>> GetGallerys([FromQuery] BrowseGallerys query)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == query.DomainId);
            
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);

        var entities = await _galleryRepository.GetAllAsync();
        entities = entities.Where(e=>e.SchoolId == school.Id).Include(e => e.Files);
        var galleries = await entities.PaginateAsync(query);
        return galleries?.Map(e => e.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GalleryDto>> GetGallery(int id)
    {
        var entity = await _galleryRepository.GetAllAsync();
        var gallery = entity.Include(e=>e.Files).FirstOrDefault(e => e.Id == id);
        if (gallery == null)
        {
            return NotFound();
        }
        return Ok(gallery.AsDto());
    }

}