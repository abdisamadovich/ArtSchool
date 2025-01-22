using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries;

public class BrowseBanners : PagedQueryBase
{
    public int? SchoolId { get; set; }
    public string DomainId { get; set; }
    public string SearchText { get; set; }
    public bool IsPublished { get; set; }
}