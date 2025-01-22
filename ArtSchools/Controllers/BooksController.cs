using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IRepository<Book, int> _bookRepository;
    private readonly IRepository<School, int> _schoolRepository;

    public BooksController(IRepository<Book, int> bookRepository, IRepository<School, int> schoolRepository)
    {
        _bookRepository = bookRepository;
        _schoolRepository = schoolRepository;
    }

    [HttpGet]
    public async Task<PagedResult<BookDto>> GetBooks([FromQuery] BrowseBooks query)
    {
        var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == query.DomainId);
            
        if(school == null)
            throw new UIException(new Language(
                $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                $"Школа с таким доменом не существует!",  // Russian (Ru)
                $"No school exists with this domain!" // English (En)
            ), StatusCodes.Status404NotFound);
        
        var entities = await _bookRepository.GetAllAsync();
        entities = entities.Include(e => e.File)
            .Where(e=>e.SchoolId == school.Id);

        var books = await entities.PaginateAsync(query);
        return books?.Map(b => b.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var entity = await _bookRepository.GetAllAsync();
        var book = entity.FirstOrDefault(e => e.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book.AsDto());
    }
}