using ArtSchools.App.Globalization;
using ArtSchools.Dashboard.Commands.Identity;
using ArtSchools.Entities;

namespace ArtSchools.Dashboard.Dtos;

public class UserDto
{
    public UserDto()
    {
        
    }
    public UserDto(int id, string login, string firstName, string lastName, string middleName, string phoneNumber, RoleDto role)
    {
        Id = id;
        Login = login;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        PhoneNumber = phoneNumber;
        Role = role;
    }

    public int Id { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public RoleDto Role { get; set; }
    public int SchoolId { get; set; }
    public SchoolInfoDto School { get; set; }
}

public class SchoolInfoDto
{
    public int Id { get; set; }
    public Language Name { get; set; }
}