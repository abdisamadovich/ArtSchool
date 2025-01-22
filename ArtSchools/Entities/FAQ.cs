using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("faqs", Schema = "schools")]
public class FAQ : IIdentifiable<int>
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
    public School School { get; set; }
    public int SchoolId { get; set; }
}