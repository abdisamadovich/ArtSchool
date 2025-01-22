using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;
[Table("file", Schema = "schools")]
public class File :  IIdentifiable<int>
{
    public int Id { get; set; }
    public string Mime { get; set; }
    public string Extension { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
    public int SchoolId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Gallery Gallery { get; set; }
    public int? GalleryId { get; set; }
}