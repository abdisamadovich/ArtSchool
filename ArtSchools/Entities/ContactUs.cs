using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("contact_us", Schema = "schools")]
public class ContactUs : IIdentifiable<int>
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public int SchoolId { get; set; }
    public School School { get; set; }
    public bool IsNew { get; set; }
}