using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class DirectionDto
    {
        public int Id { get; set; }
        public Language Name { get; set; }
        public Language Description { get; set; }
        public int SchoolId { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }
    }
}
