using ArtSchools.Entities;
using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos
{
    public class TopStudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Language Position { get; set; }
        public int SchoolId { get; set; }
        public string ImageUrl { get; set; }
        public int ImageId { get; set; }
    }
}
