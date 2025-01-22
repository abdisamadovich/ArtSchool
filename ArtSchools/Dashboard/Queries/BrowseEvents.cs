using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseEvents : PagedQueryBase
    {
        public string DomainId { get; set; }
    }
}
