using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;
[Table("users", Schema = "identity")]
public class User : IIdentifiable<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public IEnumerable<UserRole> UserRoles { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public School School { get; set; }
    public int SchoolId { get; set; }
}