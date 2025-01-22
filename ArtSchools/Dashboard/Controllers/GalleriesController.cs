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
using File = ArtSchools.Entities.File;

namespace ArtSchools.Dashboard.Controllers
{
    [Route("dashboard/[controller]")]
    [ApiController]
    [Authorize]
    public class GalleriesController : ControllerBase
    {
        private readonly IRepository<Gallery, int> _galleryRepository;
        private readonly IRepository<Entities.File, int> _fileRepository;

        public GalleriesController(IRepository<Gallery, int> galleryRepository, IRepository<File, int> fileRepository)
        {
            _galleryRepository = galleryRepository;
            _fileRepository = fileRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GalleryDto>>> GetGallerys([FromQuery] BrowseGallerys query)
        {
            var schoolId = this.User.GetOrgId();
            if (schoolId == null)
                return Forbid();
            
            var entities = await _galleryRepository.GetAllAsync();
            entities = entities.Where(e=>e.SchoolId == schoolId).Include(e => e.Files);
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

        [HttpPost]
        public async Task<ActionResult<GalleryDto>> CreateGallery(UpsertGallery command)
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
            await _galleryRepository.InsertAsync(entity);
            
            var fileIds = command.Files.Select(e => e.Id).ToArray();
            var files = (await _fileRepository.GetAllAsync())
                .Where(f => fileIds.Contains(f.Id)).ToList();
            
            foreach (var file in files)
            {
                file.GalleryId = entity.Id;
                await _fileRepository.UpdateAsync(file);
            }
            
            return CreatedAtAction(nameof(GetGallery), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGallery(int id, UpsertGallery command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _galleryRepository.GetAsync(id);
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
            
            var fileIds = command.Files.Select(e => e.Id).ToArray();
            
            var existingFiles = (await _fileRepository.GetAllAsync()).Where(f => f.GalleryId == id).ToList();

            var filesToRemove = (await _fileRepository.GetAllAsync()).Where(f => !fileIds.Contains(f.Id)).ToList();

            var filesToAdd = fileIds.Where(fid => !existingFiles.Any(ef => ef.Id == fid)).ToList();

            foreach (var file in filesToRemove)
            {
                file.GalleryId = null;
            }

            foreach (var fileId in filesToAdd)
            {
                var file = await _fileRepository.GetAsync(fileId);
                if (file != null)
                {
                    file.GalleryId = id;
                }
            }
            
            await _galleryRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGallery(int id)
        {
            var entity = await _galleryRepository.GetAsync(id);
            if (entity.SchoolId != this.User.GetOrgId() && !this.User.HasPermission("ALL_ACTIONS"))
            {
                throw new UIException(new Language(
                    "Ruxsat berilmadi!",   // Latin Uzbek (Oz)
                    "Рухсат берилмади!",   // Cyrillic Uzbek (Uz)
                    "Доступ запрещен!",    // Russian (Ru)
                    "Access denied!"       // English (En)),
                ), StatusCodes.Status403Forbidden);
            }
            await _galleryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
