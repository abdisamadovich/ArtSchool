using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("district", Schema = "schools")]
public class District : IIdentifiable<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string NameOz { get; set; }
    public string NameUz { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }
    public Region Region { get; set; }
    public int RegionId { get; set; }
    public IList<School> Schools { get; set; }
}