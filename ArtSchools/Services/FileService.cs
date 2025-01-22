namespace ArtSchools.Services;

public class FileService : IFileService
{
    public async Task<string> CreateFile(IFormFile file, string subFolder)
    {
        var storagePath = Path.Combine("app", "storage", subFolder);
            
        if (!Directory.Exists(storagePath))
            Directory.CreateDirectory(storagePath);
            
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var uniqueFileName = $"{timestamp}_{file.FileName}";
        var filePath = Path.Combine(storagePath, uniqueFileName);
            
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        
        return $"/app/storage/{subFolder}/{uniqueFileName}";
    }
    
    public void DeleteFile(string filePath)
    {
        var dirs = filePath.Split('/');
        var path = Path.Combine(dirs);
            
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);
    }
}