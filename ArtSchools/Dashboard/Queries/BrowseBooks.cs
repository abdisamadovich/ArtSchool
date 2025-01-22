using System.ComponentModel.DataAnnotations;
using ArtSchools.App.Pagination.Base;

namespace ArtSchools.Dashboard.Queries
{
    public class BrowseBooks : PagedQueryBase
    {
        public string DomainId { get; set; }
    }
}
