using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("employees", Schema = "schools")]
public class Employee : IIdentifiable<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public bool IsHead { get; set; }
    public string PhoneNumber { get; set; }
    public string PositionOz { get; set; }
    public string PositionUz { get; set; }
    public string PositionRu { get; set; }
    public string PositionEn { get; set; }
    public School School { get; set; }
    public int SchoolId { get; set; }
    public string ImageUrl { get; set; }
    public File Image { get; set; }
    public int ImageId { get; set; }
}