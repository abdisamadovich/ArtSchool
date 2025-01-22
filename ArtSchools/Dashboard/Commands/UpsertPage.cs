using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Commands;

public class UpsertPage
{
    public int Id { get; set; }
    public string Url { get; set; }
    public Language Name { get; set; }
    public Language Content { get; set; }
    public int SchoolId { get; set; }
}