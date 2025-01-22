using ArtSchools.App.Globalization;
using ArtSchools.Dashboard.Dtos;

namespace ArtSchools.Dashboard.Commands
{
    public class UpsertGallery
    {
        public int Id { get; set; }
        public Language Title { get; set; }
        public List<FileDto> Files { get; set; }
        public int SchoolId { get; set; }
    }
}
