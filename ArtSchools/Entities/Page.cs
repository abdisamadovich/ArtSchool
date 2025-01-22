using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("pages", Schema = "schools")]
public class Page : IIdentifiable<int>
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string NameOz { get; set; }
    public string NameUz { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }
    public string ContentOz { get; set; }
    public string ContentUz { get; set; }
    public string ContentRu { get; set; }
    public string ContentEn { get; set; }
    public int SchoolId { get; set; }
    public School School { get; set; }
}