using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("books", Schema = "schools")]
public class Book : IIdentifiable<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public School School { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int SchoolId { get; set; }
    public string FileUrl { get; set; }
    public string ImageUrl { get; set; }
    public File Image { get; set; }
    public int ImageId { get; set; }
    public File File { get; set; }
    public int FileId { get; set; }
}