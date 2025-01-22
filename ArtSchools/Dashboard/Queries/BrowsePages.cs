using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries;

public class BrowsePages : PagedQueryBase
{
    public string DomainId { get; set; }
    public string SearchText { get; set; }
}