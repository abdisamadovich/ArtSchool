using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries;

public class BrowseContactUs : PagedQueryBase
{
    public string SearchText { get; set; }
}