using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class FAQDto
    {
        public int Id { get; set; }
        public Language Title { get; set; }
        public Language Description { get; set; }
        public int SchoolId { get; set; }
    }
}
