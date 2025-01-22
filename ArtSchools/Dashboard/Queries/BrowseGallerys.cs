using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseGallerys : PagedQueryBase
    {
        public string DomainId { get; set; }
    }
}
