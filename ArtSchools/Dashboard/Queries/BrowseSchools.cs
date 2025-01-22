using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries;

public class BrowseSchools : PagedQueryBase
{
    public int? RegionId  { get; set; }
    public int? DistrictId { get; set; }
    public string SearchText { get; set; }
}