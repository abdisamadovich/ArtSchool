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

namespace ArtSchools.Dashboard.Controllers;

[Route("dashboard/[controller]")]
[ApiController]
[Authorize]
public class AnnouncementsController : ControllerBase
{
    private readonly IRepository<Announcement, int> _announcementRepository;
        private readonly IFileService _fileService;

        public AnnouncementsController(IRepository<Announcement, int> announcementRepository, IFileService fileService)
        {
            _announcementRepository = announcementRepository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetAnnouncements([FromQuery] BrowseNewss query)
        {
            var entities = await _announcementRepository.GetAllAsync();
            
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
        public async Task<ActionResult<AnnouncementDto>> GetAnnouncement(int id)
        {
            var entity = await _announcementRepository.GetAsync(id);
            
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
        public async Task<ActionResult<AnnouncementDto>> CreateAnnouncement(UpsertAnnouncement command)
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
            
            await _announcementRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetAnnouncement), new { id = entity.Id }, entity.AsDto());
        }
       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, UpsertAnnouncement command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _announcementRepository.GetAsync(id);
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

            await _announcementRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var entity = await _announcementRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _announcementRepository.DeleteAsync(id);
            return NoContent();
        }
}