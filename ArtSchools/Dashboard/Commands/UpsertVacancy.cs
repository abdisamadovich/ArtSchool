using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Commands
{
    public class UpsertVacancy
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
