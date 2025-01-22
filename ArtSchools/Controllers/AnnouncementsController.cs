using ArtSchools.App.Exceptions;
using ArtSchools.App.Globalization;
using ArtSchools.App.Pagination.Base;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using ArtSchools.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Controllers;

[Route("[controller]")]
[ApiController]
public class AnnouncementsController : ControllerBase
{
    private readonly IRepository<Announcement, int> _announcementRepository;
        private readonly IFileService _fileService;
        private readonly IRepository<School, int> _schoolRepository;
        public AnnouncementsController(IRepository<Announcement, int> announcementRepository, IFileService fileService, IRepository<School, int> schoolRepository)
        {
            _announcementRepository = announcementRepository;
            _fileService = fileService;
            _schoolRepository = schoolRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetAnnouncements([FromQuery] BrowseNewss query)
        {
            var school = (await _schoolRepository.GetAllAsync()).FirstOrDefault(s => s.DomainId == query.DomainId);
            
            if(school == null)
                throw new UIException(new Language(
                    $"Ushbu domenli maktab mavjud emas!",   // Latin Uzbek (Oz)
                    $"Ушбу доменли мактаб мавжуд эмас!",   // Cyrillic Uzbek (Uz)
                    $"Школа с таким доменом не существует!",  // Russian (Ru)
                    $"No school exists with this domain!" // English (En)
                ), StatusCodes.Status404NotFound);
            
            var entities = await _announcementRepository.GetAllAsync();
            
            entities = entities.Where(e => e.SchoolId == school.Id && e.IsPublished);
            
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
}