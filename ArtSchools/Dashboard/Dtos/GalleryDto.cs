using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class GalleryDto
    {
        public int Id { get; set; }
        public Language Title { get; set; }
        public List<FileDto> Files { get; set; }
        public int SchoolId { get; set; }
    }

    public class FileDto
    {
        public int Id { get; set; }
        public string FileUrl { get; set; }
    }
}
