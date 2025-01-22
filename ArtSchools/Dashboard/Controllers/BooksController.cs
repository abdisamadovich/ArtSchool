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
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book, int> _bookRepository;

        public BooksController(IRepository<Book, int> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BookDto>>> GetBooks([FromQuery] BrowseBooks query)
        {
            var schoolId = this.User.GetOrgId();
            if (schoolId == null)
                return Forbid();
            
            var entities = await _bookRepository.GetAllAsync();
            entities = entities.Include(e => e.File)
                .Where(e=>e.SchoolId == schoolId);

            var books = await entities.PaginateAsync(query);
            return books?.Map(b => b.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var entity = await _bookRepository.GetAllAsync();
            var book = entity.FirstOrDefault(e=>e.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(UpsertBook command)
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
            await _bookRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetBook), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpsertBook command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _bookRepository.GetAllAsync();
            
            var book = entity.FirstOrDefault(e=>e.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            if (book.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            book = command.AsEntity();
            await _bookRepository.UpdateAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var entity = await _bookRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _bookRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
