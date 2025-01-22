using ArtSchools.App;
using ArtSchools.App.Exeption;
using ArtSchools.App.Models;
using ArtSchools.Auth;
using ArtSchools.Entities.Context;
using ArtSchools.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

public class Program
{
    public static async Task Main(string[] args)
        => await WebHost.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .DisallowCredentials();
                    });
                });
                services.AddSingleton<IJwtHandler, JwtHandler>();
                services.AddSingleton<IJwtProvider, JwtProvider>();
                services.AddSingleton<IPasswordService, PasswordService>();
                services.AddSingleton<IPasswordHasher<IPasswordService>, PasswordHasher<IPasswordService>>();
                services.AddTransient<IIdentityService, IdentityService>();
                services.AddTransient<IRefreshTokenService, RefreshTokenService>();
                services.AddSingleton<IRng, Rng>();
                services.AddSingleton<IFileService, FileService>();

                services.AddControllers(options => options.Filters.Add<AllowPermissionFilter>());
                services.AddEndpointsApiExplorer()
                    .AddAppBuilder()
                    .AddJwt()
                    .AddRepository()
                    .AddPostgreSql();
                services.AddAuthorization();
                services.AddHttpContextAccessor();

                services.AddSwaggerGen(c =>
                {
                    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "JWT Authorization header using the Bearer scheme."
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                    {Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                            },
                            new string[] { }
                        }
                    });

                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "dev"
                        || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "master")
                        c.DocumentFilter<SwaggerBasePathFilter>("/api");
                });
            })
            .Configure(app =>
                app.UseCors("CorsPolicy")
                    .UseMiddleware<ExceptionMiddleware>()
                    .UseSwagger()
                    .UseSwaggerUI()
                    .UseAppBuilder()
                    .UseHttpsRedirection()
                    .UseAuthentication()
                    .UseRouting()
                    .UseAuthorization()
                    .UsePostgreSql()
                    .UseInitialDb()
                    .UseEndpoints(endpoints => endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"))
            ).Build().RunAsync();
}