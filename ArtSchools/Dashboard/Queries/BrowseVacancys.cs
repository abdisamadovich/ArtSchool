using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseVacancys : PagedQueryBase
    {
        public int SchoolId { get; set; }
    }
}
