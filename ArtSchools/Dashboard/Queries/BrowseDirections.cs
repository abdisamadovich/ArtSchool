using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseDirections : PagedQueryBase
    {
        public string DomainId { get; set; }
        public string SearchText { get; set; }
    }
}
