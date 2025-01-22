using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public Language Name { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }
    }
}
