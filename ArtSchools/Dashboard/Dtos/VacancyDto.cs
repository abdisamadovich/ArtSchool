using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class VacancyDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public Language Title { get; set; }
        public Language Description { get; set; }
        public Language Position { get; set; }
        public Language Requirements { get; set; }
        public Language Perks { get; set; }
    }
}
