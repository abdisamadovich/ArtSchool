using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseNewss : PagedQueryBase
    {
        public string DomainId { get; set; }
        public string SearchText { get; set; }
        public bool IsPublished { get; set; }
    }
}
