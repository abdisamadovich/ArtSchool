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
    [AllowPermission("ALL_ACTIONS")]
    public class DistrictsController : ControllerBase
    {
        private readonly IRepository<District, int> _districtRepository;

        public DistrictsController(IRepository<District, int> districtRepository)
        {
            _districtRepository = districtRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetDistricts([FromQuery] BrowseDistricts query)
        {
            var entities = await _districtRepository.GetAllAsync();
            
            if (query.RegionId != null && query.RegionId != 0)
                entities = entities.Where(e => e.RegionId == query.RegionId);
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
        public async Task<ActionResult<DistrictDto>> GetDistrict(int id)
        {
            var entity = await _districtRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<DistrictDto>> CreateDistrict(UpsertDistrict command)
        {
            var entity = command.AsEntity();
            await _districtRepository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetDistrict), new { id = entity.Id }, entity.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistrict(int id, UpsertDistrict command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var entity = await _districtRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            entity = command.AsEntity();
            await _districtRepository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            await _districtRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
