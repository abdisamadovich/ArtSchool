using ArtSchools.App.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace ArtSchools.Dashboard.Commands
{
    public class UpsertNews
    {
        public int Id { get; set; }
        public Language Title { get; set; }
        public Language Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublished { get; set; }
        public int SchoolId { get; set; }
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public Language ShortDescription { get; set; }
    }
}
