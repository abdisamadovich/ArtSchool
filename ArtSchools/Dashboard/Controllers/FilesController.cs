using ArtSchools.App.Utility;
using ArtSchools.Dashboard.Commands.Identity;
using ArtSchools.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using File = ArtSchools.Entities.File;

namespace ArtSchools.Dashboard.Controllers;
[Route("dashboard/[controller]")]
[ApiController]
[Authorize]
public class FilesController
{
    private readonly IRepository<Entities.File, int> _fileRepository;

    public FilesController(IRepository<Entities.File, int> fileRepository)
    {
        _fileRepository = fileRepository;
    }

    [HttpPost]
    [RequestSizeLimit(100_000_000_000)]
    public async Task<ActionResult<File>> AddProjectFile([FromForm]UploadFile model)
    {
        var filePath = FileUploader.Upload(model.SchoolId.ToString(), model.File);
        var file = new File()
        {
            Size = model.File.Length,
            Extension = Path.GetExtension(model.File.FileName),
            Mime = MimeTypeMap.GetMimeType(Path.GetExtension(model.File.FileName)),
            SchoolId = model.SchoolId,
            Path = filePath,
            CreatedAt = DateTime.Now
        };
        await _fileRepository.InsertAsync(file);
        return file;
    }
}