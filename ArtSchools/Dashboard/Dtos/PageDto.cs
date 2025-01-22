using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Dtos;

public class PageDto
{
    public int Id { get; set; }
    public Language Name { get; set; }
    public Language Content { get; set; }
    public int SchoolId { get; set; }
    public string Url { get; set; }
}