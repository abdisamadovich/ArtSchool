namespace ArtSchools.Dashboard.Commands.Identity;
using Microsoft.AspNetCore.Http;

public class UploadFile
{
    public IFormFile File  { get; set; }
    public int SchoolId { get; set; }
}