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
    public class BannersController : ControllerBase
    {
        private readonly IRepository<Banner, int> _bannerRepository;
        private readonly IFileService _fileService;

        public BannersController(IRepository<Banner, int> bannersRepository, IFileService fileService)
        {
            _bannerRepository = bannersRepository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BannerDto>>> GetBanners([FromQuery] BrowseBanners query)
        {
            var entities = await _bannerRepository.GetAllAsync();
            
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
            return Ok(entityDtos?.Map(e=>e.AsDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BannerDto>> GetBanners(int id)
        {
            var entity = await _bannerRepository.GetAsync(id);
            
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
        public async Task<ActionResult<BannerDto>> CreateBanners(UpsertBanner command)
        {
            var entity = command.AsEntity();
            if (command.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            
            await _bannerRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetBanners), new { id = entity.Id }, entity.AsDto());
        }
       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanners(int id, UpsertBanner command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _bannerRepository.GetAsync(id);
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

            await _bannerRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanners(int id)
        {
            var entity = await _bannerRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _bannerRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
