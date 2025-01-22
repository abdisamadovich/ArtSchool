namespace ArtSchools.Services;

public interface IFileService
{
    Task<string> CreateFile(IFormFile file, string subFolder);
    void DeleteFile(string filePath);
}