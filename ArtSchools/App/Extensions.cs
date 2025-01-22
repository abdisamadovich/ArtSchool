using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using ArtSchools.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ArtSchools.App;

public static class Extensions
    {
        private const string SectionName = "app";
        
        public static IAppBuilder AddAppBuilder(this IServiceCollection services, string sectionName = SectionName,
            IConfiguration configuration = null)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var builder = AppBuilder.Create(services, configuration);

            builder.Services.AddMemoryCache();
            Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"Art schools 1.0"));
            Console.WriteLine($"Process Id: {Process.GetCurrentProcess().Id}");
            Console.WriteLine($"Run at: Now: {DateTime.Now:dd.MM.yy HH:mm:ss}");
            Console.WriteLine($"Run at: UtcNow: {DateTime.UtcNow:dd.MM.yy HH:mm:ss}");
            Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            
            Console.WriteLine($"Assembly version: {Assembly.GetExecutingAssembly().GetName().Version}");
            
            return builder;
        }
        public static IApplicationBuilder UseAppBuilder(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            return app;
        }

        public static IWebHostBuilder AddAppSettings(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", false)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();
            });
            return builder;
        }
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }
        public static TModel GetOptions<TModel>(this IAppBuilder builder, string settingsSectionName)
            where TModel : new()
        {
            if (builder.Configuration is not null)
            {
                return builder.Configuration.GetOptions<TModel>(settingsSectionName);
            }

            using var serviceProvider = builder.Services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<TModel>(settingsSectionName);
        }
        
        public static IAppBuilder AddRepository(this IAppBuilder builder) 
        {
            builder.Services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            return builder;
        }
        
        public static ModelBuilder UseSnakeCaseNamingConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Convert C# property names to snake_case for PostgreSQL columns
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }
            }
            return modelBuilder;
        }
        
        private static string ToSnakeCase(this string str)
            => Regex.Replace(str, "(?<=.)([A-Z])", "_$1").ToLower();
    }