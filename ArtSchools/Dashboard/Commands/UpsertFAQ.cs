using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Commands
{
    public class UpsertFAQ
    {
        public int Id { get; set; }
        public Language Title { get; set; }
        public Language Description { get; set; }
        public int SchoolId { get; set; }
    }
}
