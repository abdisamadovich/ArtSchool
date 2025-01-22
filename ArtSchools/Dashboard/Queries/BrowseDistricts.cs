using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseDistricts : PagedQueryBase
    {
        public int? RegionId { get; set; }

        public string SearchText { get; set; }
        // Additional properties for filtering can be added here
    }
}
