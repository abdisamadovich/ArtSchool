using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseEmployees : PagedQueryBase
    {
        public string DomainId { get; set; }
    }
}
