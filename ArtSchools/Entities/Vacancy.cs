using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("vacancies", Schema = "schools")]
public class Vacancy : IIdentifiable<int>
{
    public int Id { get; set; }
    public int SchoolId { get; set; }
    public School School { get; set; }
    public string TitleOz { get; set; }
    public string TitleUz { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionOz { get; set; }
    public string DescriptionUz { get; set; }
    public string DescriptionRu { get; set; }
    public string DescriptionEn { get; set; }
    public string PositionOz { get; set; }
    public string PositionUz { get; set; }
    public string PositionRu { get; set; }
    public string PositionEn { get; set; }
    public string RequirementsOz { get; set; }
    public string RequirementsUz { get; set; }
    public string RequirementsRu { get; set; }
    public string RequirementsEn { get; set; }
    public string PerksOz { get; set; }
    public string PerksUz { get; set; }
    public string PerksRu { get; set; }
    public string PerksEn { get; set; }
}