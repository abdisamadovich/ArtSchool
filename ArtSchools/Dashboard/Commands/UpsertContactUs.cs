namespace ArtSchools.Dashboard.Commands;

public class UpsertContactUs
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public string DomainId { get; set; }
}