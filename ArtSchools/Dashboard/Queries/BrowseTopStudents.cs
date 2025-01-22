using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseTopStudents : PagedQueryBase
    {
        public string DomainId { get; set; }
    }
}
