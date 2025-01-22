using ArtSchools.App;
using ArtSchools.App.Models;
using ArtSchools.Repositories.Base;
using ArtSchools.Services;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.Entities.Context;

public static class Extensions
{
    
    public static IAppBuilder AddPostgreSql(this IAppBuilder builder) 
    {
        var sqlOptions = builder.GetOptions<PostgreSqlOptions>("postgreSQL");
        
        var connectionString =
            $"Host={sqlOptions.Host};Port={sqlOptions.Port};Database={sqlOptions.Database};Username={sqlOptions.User};Password={sqlOptions.Password}";
        
        builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(connectionString));
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
        builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return builder;
    }
    
    public static IApplicationBuilder UsePostgreSql(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
        }

        return app;
    }
    
    public static IApplicationBuilder UseInitialDb(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
            
            identityService.InsertAdmin().GetAwaiter().GetResult();
        }

        return app;
    }
}