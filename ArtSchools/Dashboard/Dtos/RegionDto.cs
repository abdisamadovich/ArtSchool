using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class RegionDto
    {
        public int Id { get; set; }
        public Language Name { get; set; }
        public IList<District> Districts { get; set; }
    }
}
