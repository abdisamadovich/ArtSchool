using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("galleries", Schema = "schools")]
public class Gallery : IIdentifiable<int>
{
    public int Id { get; set; }
    public string TitleOz { get; set; }
    public string TitleUz { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public School School { get; set; }
    public int SchoolId { get; set; }
    public List<File> Files { get; set; }
}