using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Commands
{
    public class UpsertDirection
    {
        public int Id { get; set; }
        public Language Name { get; set; }
        public Language Description { get; set; }
        public int SchoolId { get; set; }
        public string ImageUrl { get; set; }
        public int ImageId { get; set; }
    }
}
