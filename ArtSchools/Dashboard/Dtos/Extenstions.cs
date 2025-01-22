using ArtSchools.App.Globalization;
using ArtSchools.Dashboard.Commands.Identity;
using ArtSchools.Entities;

namespace ArtSchools.Dashboard.Dtos;

public static class Extenstions
{
    public static UserDto AsDto(this User user)
    {
        var role = user.UserRoles.FirstOrDefault().Role.Name;
        return new UserDto()
        {
            Id = user.Id,
            Login = user.Login,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            PhoneNumber = user.PhoneNumber,
            SchoolId = user.SchoolId,
            Role = user.UserRoles.AsDto(),
            School = user.School.AsInfoDto(),
        };
    }

    public static SchoolInfoDto AsInfoDto(this School school)
    {
        return new SchoolInfoDto
        {
            Id = school.Id,
            Name = new Language(school.NameOz, school.NameUz, school.NameRu, school.NameEn)
        };
    }

    public static RoleDto AsDto(this IEnumerable<UserRole> userRoles)
    {
        var role = userRoles.FirstOrDefault();
        return new RoleDto()
        {
            Id = role.Role.Id,
            Name = role.Role.Name,
            Permissions = role.Role.RolePermissions.AsDto()
        };
    }
    public static List<PermissionDto> AsDto(this IEnumerable<RolePermission> userRoles)
    {
        var permissions = userRoles.Select(ur => new PermissionDto(){Id = ur.Permission.Id, Permission = ur.Permission.PermissionName}).ToList();
        return permissions;
    }
    public static SchoolDto AsDto(this School school)
    {
        return new SchoolDto()
        {
            Id = school.Id,
            DomainId = school.DomainId,
            Number = school.Number,
            Name = new Language(school.NameOz, school.NameUz, school.NameRu, school.NameEn),
            Address = school.Address,
            Email = school.Email,
            SiteLink = school.SiteLink,
            WorkingHoursStart = school.WorkingHoursStart,
            WorkingHoursEnd = school.WorkingHoursEnd,
            WorkingDaysStart = school.WorkingDaysStart,
            WorkingDaysEnd = school.WorkingDaysEnd,
            LunchStart = school.LunchStart,
            LunchEnd = school.LunchEnd,
            DistrictId = school.DistrictId,
            RegionId = school.RegionId,
            TelegramLink = school.TelegramLink,
            InstagramLink = school.InstagramLink,
            FacebookLink = school.FacebookLink,
            YoutubeLink = school.YoutubeLink,
            ImageUrl = school.ImageUrl,
            ImageId = school.ImageId,
            VideoLink = school.VideoLink,
            Description = new Language(school.DescriptionOz, school.DescriptionUz, school.DescriptionRu,
                school.DescriptionEn),
            ShortDescription = new Language(school.ShortDescriptionOz, school.ShortDescriptionUz,
                school.ShortDescriptionRu, school.ShortDescriptionEn),
            Map = school.Map,
            ClassCount = school.ClassCount,
            TeacherCount = school.TeacherCount,
            SpecialClassCount = school.SpecialClassCount,
            StudentCount = school.StudentCount,
            Years = school.Years,
            AgeLvlOne = school.AgeLvlOne,
            AgeLvlTwo = school.AgeLvlTwo,
            AgeLvlThree = school.AgeLvlThree,
            AgeLvlFour = school.AgeLvlFour,
            AgeLvlFive = school.AgeLvlFive,
            AgeLvlSix = school.AgeLvlSix,
            PhoneNumber = school.PhoneNumber,
            BoyCount = school.BoyCount,
            GirlCount = school.GirlCount
        };
    }
       
        public static DirectionDto AsDto(this Direction entity)
        {
            return new DirectionDto
            {
                Id = entity.Id,
                Name = new Language(entity.NameOz, entity.NameUz, entity.NameRu, entity.NameEn),
                Description = new Language(entity.DescriptionOz, entity.DescriptionUz, entity.DescriptionRu, entity.DescriptionEn),
                SchoolId = entity.SchoolId,
                ImageId = entity.ImageId,
                ImageUrl = entity.ImageUrl
            };
        }

        public static TopStudentDto AsDto(this TopStudent entity)
        {
            return new TopStudentDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Position = new Language(entity.PositionOz, entity.PositionUz, entity.PositionRu, entity.PositionEn),
                SchoolId = entity.SchoolId,
                ImageUrl = entity.ImageUrl,
                ImageId = entity.ImageId
            };
        }

        public static FAQDto AsDto(this FAQ entity)
        {
            return new FAQDto
            {
                Id = entity.Id,
                Title = new Language(entity.TitleOz, entity.TitleUz, entity.TitleRu, entity.TitleEn),
                Description = new Language(entity.DescriptionOz, entity.DescriptionUz, entity.DescriptionRu, entity.DescriptionEn),
                SchoolId = entity.SchoolId
            };
        }
        public static GalleryDto AsDto(this Gallery entity)
        {
            return new GalleryDto
            {
                Id = entity.Id,
                Title = new Language(entity.TitleOz, entity.TitleUz, entity.TitleRu, entity.TitleEn),
                SchoolId = entity.SchoolId,
                Files = entity.Files.Select(x => x.AsDto()).ToList()
            };
        }
        public static FileDto AsDto(this Entities.File entity)
        {
            return new FileDto
            {
                Id = entity.Id,
                FileUrl = entity.Path
            };
        }
        public static NewsDto AsDto(this News entity)
        {
            return new NewsDto
            {
                Id = entity.Id,
                IsPublished = entity.IsPublished,
                SchoolId = entity.SchoolId,
                CreatedAt = entity.CreatedAt,
                ImageUrl = entity.ImageUrl,
                Title = new Language(entity.TitleOz, entity.TitleUz, entity.TitleRu, entity.TitleEn),
                Description = new Language(entity.DescriptionOz, entity.DescriptionUz, entity.DescriptionRu, entity.DescriptionEn),
                ImageId = entity.ImageId,
                ShortDescription = new Language(entity.ShortDescriptionOz, entity.ShortDescriptionUz, entity.ShortDescriptionRu, entity.ShortDescriptionEn)
            };
        }
        public static AnnouncementDto AsDto(this Announcement entity)
        {
            return new AnnouncementDto()
            {
                Id = entity.Id,
                IsPublished = entity.IsPublished,
                SchoolId = entity.SchoolId,
                CreatedAt = entity.CreatedAt,
                ImageUrl = entity.ImageUrl,
                Title = new Language(entity.TitleOz, entity.TitleUz, entity.TitleRu, entity.TitleEn),
                Description = new Language(entity.DescriptionOz, entity.DescriptionUz, entity.DescriptionRu, entity.DescriptionEn),
                ImageId = entity.ImageId,
                ShortDescription = new Language(entity.ShortDescriptionOz, entity.ShortDescriptionUz, entity.ShortDescriptionRu, entity.ShortDescriptionEn)
            };
        }

        public static PageDto AsDto(this Page entity)
        {
            return new PageDto()
            {
                Id = entity.Id,
                Content = new Language(entity.ContentOz, entity.ContentUz, entity.ContentRu, entity.ContentEn),
                Name = new Language(entity.NameOz, entity.NameUz, entity.NameRu, entity.NameEn),
                Url = entity.Url,
                SchoolId = entity.SchoolId
            };
        }

        public static ContactUsDto AsDto(this ContactUs entity)
        {
            return new ContactUsDto()
            {
                Id = entity.Id,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Email = entity.Email,
                Message = entity.Message,
                PhoneNumber = entity.PhoneNumber,
                SchoolId = entity.SchoolId,
                IsNew = entity.IsNew
            };
        }
        public static BannerDto AsDto(this Banner entity)
        {
            return new BannerDto()
            {
                Id = entity.Id,
                IsPublished = entity.IsPublished,
                SchoolId = entity.SchoolId,
                CreatedAt = entity.CreatedAt,
                ImageUrl = entity.ImageUrl,
                Title = new Language(entity.TitleOz, entity.TitleUz, entity.TitleRu, entity.TitleEn),
                Description = new Language(entity.DescriptionOz, entity.DescriptionUz, entity.DescriptionRu, entity.DescriptionEn),
                Link = entity.Link,
                ImageId = entity.ImageId,
                ShortDescription = new Language(entity.ShortDescriptionOz, entity.ShortDescriptionUz, entity.ShortDescriptionRu, entity.ShortDescriptionEn)
            };
        }
        public static EmployeeDto AsDto(this Employee entity)
        {
            return new EmployeeDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                Email = entity.Email,
                IsHead = entity.IsHead,
                PhoneNumber = entity.PhoneNumber,
                Position = new Language(entity.PositionOz, entity.PositionUz, entity.PositionRu, entity.PositionEn),
                SchoolId = entity.SchoolId,
                ImageUrl = entity.ImageUrl,
                ImageId = entity.ImageId,
            };
        }
        public static BookDto AsDto(this Book entity)
        {
            return new BookDto
            {
                Id = entity.Id,
                FileUrl = entity.FileUrl,
                ImageUrl = entity.ImageUrl,
                Name = entity.Name,
                Author = entity.Author,
                Description = entity.Description,
                Year = entity.Year,
                SchoolId = entity.SchoolId,
                ImageId = entity.ImageId,
                FileId = entity.FileId,
            };
        }
        public static RegionDto AsDto(this Region entity)
        {
            return new RegionDto
            {
                Id = entity.Id,
                Name = new Language(entity.NameOz, entity.NameUz, entity.NameRu, entity.NameEn)
            };
        }
        public static DistrictDto AsDto(this District entity)
        {
            return new DistrictDto
            {
                Id = entity.Id,
                RegionId = entity.RegionId,
                Name = new Language(entity.NameOz, entity.NameUz, entity.NameRu, entity.NameEn)
            };
        }
        public static VacancyDto AsDto(this Vacancy entity)
        {
            return new VacancyDto
            {
                Id = entity.Id,
                SchoolId = entity.SchoolId,
                Title = new Language(entity.TitleOz, entity.TitleUz, entity.TitleRu, entity.TitleEn),
                Description = new Language(entity.DescriptionOz, entity.DescriptionUz, entity.DescriptionRu, entity.DescriptionEn),
                Position = new Language(entity.PositionOz, entity.PositionUz, entity.PositionRu, entity.PositionEn),
                Requirements = new Language(entity.RequirementsOz, entity.RequirementsUz, entity.RequirementsRu, entity.RequirementsEn),
                Perks = new Language(entity.PerksOz, entity.PerksUz, entity.PerksRu, entity.PerksEn)
            };
        }
        public static EventDto AsDto(this Event entity)
        {
            return new EventDto
            {
                Id = entity.Id,
                Title = new Language(entity.TitleOz, entity.TitleUz, entity.TitleRu, entity.TitleEn),
                Description = new Language(entity.DescriptionOz, entity.DescriptionUz, entity.DescriptionRu, entity.DescriptionEn),
                CreatedAt = entity.CreatedAt,
                DateTime = entity.DateTime,
                ImageUrl = entity.ImageUrl,
                SchoolId = entity.SchoolId,
                ImageId = entity.ImageId,
                ShortDescription = new Language(entity.ShortDescriptionOz, entity.ShortDescriptionUz, entity.ShortDescriptionRu, entity.ShortDescriptionEn),
                Direction = entity.Direction == null ? null : entity.Direction.AsDto()
            };
        }
}