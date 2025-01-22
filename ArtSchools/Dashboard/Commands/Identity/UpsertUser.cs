using ArtSchools.Entities;

namespace ArtSchools.Dashboard.Commands.Identity;

public class UpsertUser
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public RoleDto Role { get; set; }
    public int SchoolId { get; set; }
}

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PermissionDto> Permissions { get; set; }
}

public class PermissionDto
{
    public int Id { get; set; }
    public string Permission { get; set; }
}