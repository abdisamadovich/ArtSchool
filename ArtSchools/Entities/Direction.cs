using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("directions", Schema = "schools")]
public class Direction : IIdentifiable<int>
{
    public int Id { get; set; }
    public string NameOz { get; set; }
    public string NameUz { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }
    public string DescriptionOz { get; set; }
    public string DescriptionUz { get; set; }
    public string DescriptionRu { get; set; }
    public string DescriptionEn { get; set; }
    public School School { get; set; }
    public int SchoolId { get; set; }
    public int? ImageId { get; set; }
    public File Image { get; set; }
    public string ImageUrl { get; set; }
    public Event Event { get; set; }
}