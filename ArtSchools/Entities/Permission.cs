using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("permissions", Schema = "identity")]
public class Permission : IIdentifiable<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string PermissionName { get; set; }
    public IEnumerable<RolePermission> RolePermissions { get; set; }
}