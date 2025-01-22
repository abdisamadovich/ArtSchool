using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("region", Schema = "schools")]
public class Region : IIdentifiable<int>
{
    public int Id { get; set; }
    public string NameOz { get; set; }
    public string NameUz { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }

    public IList<School> Schools { get; set; }
    public IList<District> Districts { get; set; }
}