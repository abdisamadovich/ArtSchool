using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Commands
{
    public class UpsertEvent
    {
        public int Id { get; set; }
        public Language Title { get; set; }
        public Language Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DateTime { get; set; }
        public int SchoolId { get; set; }
        public string ImageUrl { get; set; }
        public int ImageId { get; set; }
        public Language ShortDescription { get; set; }
        public int? DirectionId { get; set; }
    }
}
