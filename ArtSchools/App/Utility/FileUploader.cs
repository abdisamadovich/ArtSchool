namespace ArtSchools.App.Utility;

public class FileUploader
{
    public static string Upload(string dirName, IFormFile newFile)
    {
        var path = Directory.GetCurrentDirectory();
        
        path = Path.Combine(path, "wwwroot", "schoolfiles", dirName);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        byte[] bytes = null;
        using (var binaryReader = new BinaryReader(newFile.OpenReadStream()))
        {
            bytes = binaryReader.ReadBytes((int) newFile.Length);
        }

        var fileName = CombinateFileName(newFile.FileName);

        path = Path.Combine(path, fileName);
        if (bytes.Length == 0)
        {
        }

        Console.WriteLine(path);
        File.WriteAllBytes(path, bytes);
        return FileUrl(dirName, fileName);
    }

    public static string FileUrl(string dirName, string fileName)
    {
        return  "/" + "schoolfiles/" + dirName + "/" + fileName;
    }

    public static string CombinateFileName(string fileName)
    {
        var fileTip = fileName.Split('.').Last();
        var guid = $"{DateTime.Now:yyyyMMddHHmmssfff}";
        return guid + "." + fileTip;
    }
}