using ArtSchools.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ArtSchools.Entities.Context;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<School> Schools { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Direction> Directions { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<TopStudent> TopStudents { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<FAQ> Faqs { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }
    
    public DbSet<File> Files { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public Microsoft.EntityFrameworkCore.DbContext Context => this;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSnakeCaseNamingConvention();

        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new {rp.RoleId, rp.PermissionId});
        
        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);
        
        modelBuilder.Entity<UserRole>()
            .HasKey(rp => new {rp.RoleId, rp.UserId});
        
        modelBuilder.Entity<UserRole>()
            .HasOne(rp => rp.User)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(rp => rp.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(rp => rp.Role)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(rp => rp.RoleId);

        #region HasData
        
        #region Region has data
        modelBuilder.Entity<Region>().HasData(
            
            new Region
            {
                Id = 1,
                NameUz = "Тошкент шаҳри",
                NameOz = "Toshkent shahar",
                NameEn = "Tashkent City",
                NameRu = "Город Ташкент"
            },
            new Region
            {
                Id = 2,
                NameUz = "Фарғона вилояти",
                NameOz = "Farg‘ona viloyati",
                NameEn = "Fergana",
                NameRu = "Ферганская область"
            },
            new Region
            {
                Id = 3,
                NameUz = "Андижон вилояти",
                NameOz = "Andijon viloyati",
                NameEn = "Andijan",
                NameRu = "Андижанская область"
            },
            new Region
            {
                Id = 4,
                NameUz = "Бухоро вилояти",
                NameOz = "Buxoro viloyati",
                NameEn = "Bukhara",
                NameRu = "Бухарская область"
            },
            new Region
            {
                Id = 5,
                NameUz = "Жиззах вилояти",
                NameOz = "Jizzax viloyati",
                NameEn = "Jizzakh",
                NameRu = "Джизакская область"
            },
            new Region
            {
                Id = 6,
                NameUz = "Қашқадарё вилояти",
                NameOz = "Qashqadaryo viloyati",
                NameEn = "Qashqadaryo",
                NameRu = "Кашкадарьинская область"
            },
            new Region
            {
                Id = 7,
                NameUz = "Навоий вилояти",
                NameOz = "Navoiy viloyati",
                NameEn = "Navoiy",
                NameRu = "Навоийская область"
            },
            new Region
            {
                Id = 8,
                NameUz = "Наманган вилояти",
                NameOz = "Namangan viloyati",
                NameEn = "Namangan",
                NameRu = "Наманганская область"
            },
            new Region
            {
                Id = 9,
                NameUz = "Самарқанд вилояти",
                NameOz = "Samarqand viloyati",
                NameEn = "Samarqand",
                NameRu = "Самаркандская область"
            },
            new Region
            {
                Id = 10,
                NameUz = "Сурхондарё вилояти",
                NameOz = "Surxondaryo viloyati",
                NameEn = "Surxondaryo",
                NameRu = "Сурхандарьинская область"
            },
            new Region
            {
                Id = 11,
                NameUz = "Сирдарё вилояти",
                NameOz = "Sirdaryo viloyati",
                NameEn = "Sirdaryo",
                NameRu = "Сырдарьинская область"
            },
            new Region
            {
                Id = 12,
                NameUz = "Тошкент вилояти",
                NameOz = "Toshkent viloyati",
                NameEn = "Tashkent",
                NameRu = "Ташкентская область"
            },
            new Region
            {
                Id = 13,
                NameUz = "Хоразм вилояти",
                NameOz = "Xorazm viloyati",
                NameEn = "Khorezm",
                NameRu = "Хорезмская область"
            },
            new Region
            {
                Id = 14,
                NameUz = "Қорақалпоғистон Республикаси",
                NameOz = "Qoraqalpog‘iston Respublikasi",
                NameEn = "Karakalpakstan",
                NameRu = "Республика Каракалпакстан"
            }
        );
        #endregion

        #region District Has Data

        
        modelBuilder.Entity<District>().HasData(
            new District
            {
                Id = 1, NameUz = "Бектемир тумани", NameOz = "Bektemir tumani", NameRu = "Бектемирский район",
                NameEn = "Bektemir District", RegionId = 1
            },
            new District
            {
                Id = 2, NameUz = "Чиланзар тумани", NameOz = "Chilonzor tumani", NameRu = "Чиланзарский район",
                NameEn = "Chilonzor District", RegionId = 1
            },
            new District
            {
                Id = 3, NameUz = "Яшнобод тумани", NameOz = "Yashnobod tumani", NameRu = "Яшнабадский район",
                NameEn = "Yashnobod District", RegionId = 1
            },
            new District
            {
                Id = 4, NameUz = "Миробод тумани", NameOz = "Mirobod tumani", NameRu = "Мирабадский район",
                NameEn = "Mirobod District", RegionId = 1
            },
            new District
            {
                Id = 5, NameUz = "Мирзо Улуғбек тумани", NameOz = "Mirzo Ulug‘bek tumani",
                NameRu = "Мирзо-Улугбекский район", NameEn = "Mirzo Ulug‘bek District", RegionId = 1
            },
            new District
            {
                Id = 6, NameUz = "Сергели тумани", NameOz = "Sergeli tumani", NameRu = "Сергелийский район",
                NameEn = "Sergeli District", RegionId = 1
            },
            new District
            {
                Id = 7, NameUz = "Учтепа тумани", NameOz = "Uchtepa tumani", NameRu = "Учтепинский район",
                NameEn = "Uchtepa District", RegionId = 1
            },
            new District
            {
                Id = 8, NameUz = "Шайхонтоҳур тумани", NameOz = "Shayxontohur tumani",
                NameRu = "Шайхантахурский район", NameEn = "Shayxontohur District", RegionId = 1
            },
            new District
            {
                Id = 9, NameUz = "Олмазор тумани", NameOz = "Olmazor tumani", NameRu = "Алмазарский район",
                NameEn = "Olmazor District", RegionId = 1
            },
            new District
            {
                Id = 10, NameUz = "Юнусобод тумани", NameOz = "Yunusobod tumani", NameRu = "Юнусабадский район",
                NameEn = "Yunusobod District", RegionId = 1
            },
            new District
            {
                Id = 11, NameUz = "Яккасарой тумани", NameOz = "Yakkasaroy tumani", NameRu = "Яккасарайский район",
                NameEn = "Yakkasaroy District", RegionId = 1
            },
            new District
            {
                Id = 12, NameUz = "Янгиҳаёт тумани", NameOz = "Yangiha’yot tumani", NameRu = "Янгахайотский район",
                NameEn = "Yangiha’yot District", RegionId = 1
            },
            new District
            {
                Id = 13, NameUz = "Олтиариқ тумани", NameOz = "Oltiariq tumani", NameRu = "Алтыарыкский район",
                NameEn = "Oltiariq District", RegionId = 2
            },
            new District
            {
                Id = 14, NameUz = "Боғдод тумани", NameOz = "Bog‘dod tumani", NameRu = "Багдадский район",
                NameEn = "Bog‘dod District", RegionId = 2
            },
            new District
            {
                Id = 15, NameUz = "Бешариқ тумани", NameOz = "Beshariq tumani", NameRu = "Бешарыкский район",
                NameEn = "Beshariq District", RegionId = 2
            },
            new District
            {
                Id = 16, NameUz = "Бувайда тумани", NameOz = "Buvayda tumani", NameRu = "Бувайдинский район",
                NameEn = "Buvayda District", RegionId = 2
            },
            new District
            {
                Id = 17, NameUz = "Данғара тумани", NameOz = "Dang‘ara tumani", NameRu = "Дангаринский район",
                NameEn = "Dang‘ara District", RegionId = 2
            },
            new District
            {
                Id = 18, NameUz = "Қува тумани", NameOz = "Quva tumani", NameRu = "Кувинский район",
                NameEn = "Quva District", RegionId = 2
            },
            new District
            {
                Id = 19, NameUz = "Қўштепа тумани", NameOz = "Qo‘shtepa tumani", NameRu = "Куштепинский район",
                NameEn = "Qo‘shtepa District", RegionId = 2
            },
            new District
            {
                Id = 20, NameUz = "Риштон тумани", NameOz = "Rishton tumani", NameRu = "Риштанский район",
                NameEn = "Rishton District", RegionId = 2
            },
            new District
            {
                Id = 21, NameUz = "Сўх тумани", NameOz = "So‘x tumani", NameRu = "Сохский район",
                NameEn = "So‘x District", RegionId = 2
            },
            new District
            {
                Id = 22, NameUz = "Тошлоқ тумани", NameOz = "Toshloq tumani", NameRu = "Ташлакский район",
                NameEn = "Toshloq District", RegionId = 2
            },
            new District
            {
                Id = 23, NameUz = "Ўзбекистон тумани", NameOz = "O‘zbekiston tumani", NameRu = "Узбекистанский район",
                NameEn = "O‘zbekiston District", RegionId = 2
            },
            new District
            {
                Id = 24, NameUz = "Учкўприк тумани", NameOz = "Uchko‘prik tumani", NameRu = "Учкуприкский район",
                NameEn = "Uchko‘prik District", RegionId = 2
            },
            new District
            {
                Id = 25, NameUz = "Фарғона тумани", NameOz = "Farg‘ona tumani", NameRu = "Ферганский район",
                NameEn = "Farg‘ona District", RegionId = 2
            },
            new District
            {
                Id = 26, NameUz = "Фурқат тумани", NameOz = "Furqat tumani", NameRu = "Фуркатский район",
                NameEn = "Furqat District", RegionId = 1
            },
            new District
            {
                Id = 27, NameUz = "Ёзёвон тумани", NameOz = "Yozovon tumani", NameRu = "Язёванский район",
                NameEn = "Yozovon District", RegionId = 1
            });
        #endregion

        #region School HasData
        
        modelBuilder.Entity<School>().HasData(
            new School()
            {
                Id = 1,
                NameOz = "Madaniyat.uz",
                NameUz = "Madaniyat.uz",
                NameRu = "Madaniyat.uz",
                NameEn = "Madaniyat.uz",
                RegionId = 1,
                DistrictId = 5,
                Type = "MINISTRY",
            });
        #endregion

        #region Permission HasData

        modelBuilder.Entity<Permission>().HasData(
            new Permission()
            {
                Id = 1,
                PermissionName = "SCHOOL_ACTIONS"
            });
        
        #endregion

        #region Role HasData
        modelBuilder.Entity<Role>().HasData(
            new Role()
            {
                Id = 1,
                Name = "School admin"
            });
        modelBuilder.Entity<RolePermission>().HasData(
            new RolePermission()
            {
                RoleId = 1,
                PermissionId = 1
            });

        #endregion
        #endregion
        
        
        
        base.OnModelCreating(modelBuilder);
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            $"Host=172.16.60.6;Port=5432;Database=dms;Username=singlereestr;Password=singlereestr";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}