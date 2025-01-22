using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Auth;
using ArtSchools.Dashboard.Commands;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using ArtSchools.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Dashboard.Controllers
{
    [Route("dashboard/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly IRepository<News, int> _newsRepository;
        private readonly IFileService _fileService;

        public NewsController(IRepository<News, int> newsRepository, IFileService fileService)
        {
            _newsRepository = newsRepository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetNewss([FromQuery] BrowseNewss query)
        {
            var entities = await _newsRepository.GetAllAsync();
            
            var schoolId = this.User.GetOrgId();
            if (schoolId == null)
                return Forbid();
            
            entities = entities.Where(e => e.SchoolId == schoolId);
            
            if (!string.IsNullOrEmpty(query.SearchText))
            {
                query.SearchText = query.SearchText.ToLower();
                entities = entities.Where(e => e.TitleEn.ToLower().Contains(query.SearchText) ||
                                               e.TitleRu.ToLower().Contains(query.SearchText) ||
                                               e.TitleUz.ToLower().Contains(query.SearchText) ||
                                               e.TitleOz.ToLower().Contains(query.SearchText)
                );
            }
            
            var entityDtos = await entities.PaginateAsync(query);
            
            return Ok(entityDtos.Map(e=>e.AsDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNews(int id)
        {
            var entity = await _newsRepository.GetAsync(id);
            
            var isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated && !entity.IsPublished)
                return NotFound();
            
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<NewsDto>> CreateNews(UpsertNews command)
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
            
            await _newsRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetNews), new { id = entity.Id }, entity.AsDto());
        }
       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, UpsertNews command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _newsRepository.GetAsync(id);
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

            await _newsRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var entity = await _newsRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _newsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
