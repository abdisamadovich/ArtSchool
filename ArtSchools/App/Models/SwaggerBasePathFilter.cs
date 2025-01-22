using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArtSchools.App.Models;

public class SwaggerBasePathFilter : IDocumentFilter
{
    private readonly string _basePath;

    public SwaggerBasePathFilter(string basePath)
    {
        _basePath = basePath;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = _basePath }
        };
    }
}