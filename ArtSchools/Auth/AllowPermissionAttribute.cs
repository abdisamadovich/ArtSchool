namespace ArtSchools.Auth;

public class AllowPermissionAttribute : Attribute
{
    public string[] Permissions { get; }

    public AllowPermissionAttribute(params string[] permissions)
    {
        Permissions = permissions;
    }
}