using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Entities.Context;

public interface IApplicationDbContext
{
    DbContext Context { get; }
}