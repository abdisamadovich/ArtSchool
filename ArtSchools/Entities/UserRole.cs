using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("user_role", Schema = "identity")]
public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}