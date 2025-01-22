using ArtSchools.Auth;
using ArtSchools.Dashboard.Commands;
using ArtSchools.Dashboard.Dtos;
using ArtSchools.Dashboard.Queries;
using ArtSchools.Entities;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Dashboard.Controllers
{
    [Route("dashboard/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRepository<Region, int> _regionRepository;

        public RegionsController(IRepository<Region, int> regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetRegions([FromQuery] BrowseRegions query)
        {
            var entities = await _regionRepository.GetAllAsync(); 
            
            if (!string.IsNullOrEmpty(query.SearchText))
            {
                query.SearchText = query.SearchText.ToLower();
                entities = entities.Where(e => e.NameEn.ToLower().Contains(query.SearchText) ||
                                               e.NameRu.ToLower().Contains(query.SearchText) ||
                                               e.NameUz.ToLower().Contains(query.SearchText) ||
                                               e.NameOz.ToLower().Contains(query.SearchText)
                );
            }

            var entityDtos = entities.Select(entity => entity.AsDto());
            return Ok(entityDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetRegion(int id)
        {
            var entity = await _regionRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity.AsDto());
        }

        [HttpPost]
        [AllowPermission("ALL_ACTIONS")]
        public async Task<ActionResult<RegionDto>> CreateRegion(UpsertRegion command)
        {
            var entity = command.AsEntity();
            await _regionRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetRegion), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        [AllowPermission("ALL_ACTIONS")]
        public async Task<IActionResult> UpdateRegion(int id, UpsertRegion command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _regionRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            entity = command.AsEntity();
            await _regionRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [AllowPermission("ALL_ACTIONS")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            await _regionRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
