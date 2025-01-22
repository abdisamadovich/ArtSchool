using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int SchoolId { get; set; }
        public string FileUrl { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int ImageId { get; set; }
        public int FileId { get; set; }
    }
}