using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("banners", Schema = "schools")]
public class Banner : IIdentifiable<int>
{
    public int Id { get; set; }
    public string TitleOz { get; set; }
    public string TitleUz { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionOz { get; set; }
    public string DescriptionUz { get; set; }
    public string DescriptionRu { get; set; }
    public string DescriptionEn { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
    public School School { get; set; }
    public int SchoolId { get; set; }
    public string ImageUrl { get; set; }
    public File Image { get; set; }
    public int ImageId { get; set; }
    public string Link { get; set; }
    public string ShortDescriptionOz { get; set; }
    public string ShortDescriptionUz { get; set; }
    public string ShortDescriptionRu { get; set; }
    public string ShortDescriptionEn { get; set; }
}