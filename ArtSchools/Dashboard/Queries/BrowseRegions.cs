using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseRegions : PagedQueryBase
    {
        public string SearchText { get; set; }
    }
}
