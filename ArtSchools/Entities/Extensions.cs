using ArtSchools.Dashboard.Commands;

namespace ArtSchools.Entities;

public static class Extensions
{
    public static School AsEntity(this UpsertSchool command)
        => new School()
        {
            Id = command.Id,
            DomainId = command.DomainId,
            Number = command.Number,
            NameOz = command.Name.Oz,
            NameUz = command.Name.Uz,
            NameRu = command.Name.Ru,
            NameEn = command.Name.En,
            WorkingHoursStart = command.WorkingHoursStart,
            WorkingHoursEnd = command.WorkingHoursEnd,
            WorkingDaysStart = command.WorkingDaysStart,
            WorkingDaysEnd = command.WorkingDaysEnd,
            LunchStart = command.LunchStart,
            LunchEnd = command.LunchEnd,
            RegionId = command.RegionId,
            DistrictId = command.DistrictId,
            Address = command.Address,
            Email = command.Email,
            SiteLink = command.SiteLink,
            TelegramLink = command.TelegramLink,
            InstagramLink = command.InstagramLink,
            FacebookLink = command.FacebookLink,
            YoutubeLink = command.YoutubeLink,
            ImageUrl = command.ImageUrl,
            ImageId = command.ImageId,
            VideoLink = command.VideoLink,
            DescriptionOz = command.Description.Oz,
            DescriptionUz = command.Description.Uz,
            DescriptionRu = command.Description.Ru,
            DescriptionEn = command.Description.En,
            ShortDescriptionOz = command.ShortDescription.Oz,
            ShortDescriptionUz = command.ShortDescription.Uz,
            ShortDescriptionRu = command.ShortDescription.Ru,
            ShortDescriptionEn = command.ShortDescription.En,
            Map = command.Map,
            ClassCount = command.ClassCount,
            TeacherCount = command.TeacherCount,
            SpecialClassCount = command.SpecialClassCount,
            StudentCount = command.StudentCount,
            Years = command.Years,
            AgeLvlOne = command.AgeLvlOne,
            AgeLvlTwo = command.AgeLvlTwo,
            AgeLvlThree = command.AgeLvlThree,
            AgeLvlFour = command.AgeLvlFour,
            AgeLvlFive = command.AgeLvlFive,
            AgeLvlSix = command.AgeLvlSix,
            PhoneNumber = command.PhoneNumber,
            BoyCount = command.BoyCount,
            GirlCount = command.GirlCount
        };
    public static Region AsEntity(this UpsertRegion command)
        {
            return new Region
            {
                Id = command.Id,
                NameOz = command.Name.Oz,
                NameUz = command.Name.Uz,
                NameEn = command.Name.En,
                NameRu = command.Name.Ru,
            };
        }
        public static District AsEntity(this UpsertDistrict command)
        {
            return new District
            {
                Id = command.Id,
                NameOz = command.Name.Oz,
                NameUz = command.Name.Uz,
                NameEn = command.Name.En,
                NameRu = command.Name.Ru,
                RegionId = command.RegionId,
            };
        }
        public static Vacancy AsEntity(this UpsertVacancy command)
        {
            return new Vacancy
            {
                Id = command.Id,
                SchoolId = command.SchoolId,
                TitleOz = command.Title.Oz,
                TitleUz = command.Title.Uz,
                TitleRu = command.Title.Ru,
                TitleEn = command.Title.En,
                DescriptionOz = command.Description.Oz,
                DescriptionUz = command.Description.Uz,
                DescriptionRu = command.Description.Ru,
                DescriptionEn = command.Description.En,
                PositionUz = command.Position.Uz,
                PositionEn = command.Position.En,
                PositionOz = command.Position.Oz,
                PositionRu = command.Position.Ru,
                RequirementsOz = command.Requirements.Oz,
                RequirementsUz = command.Requirements.Uz,
                RequirementsRu = command.Requirements.Ru,
                RequirementsEn = command.Requirements.En,
                PerksOz = command.Perks.Oz,
                PerksUz = command.Perks.Uz,
                PerksRu = command.Perks.Ru,
                PerksEn = command.Perks.En,
            };
        }
        public static Event AsEntity(this UpsertEvent command)
        {
            return new Event
            {
                Id = command.Id,
                TitleOz = command.Title.Oz,
                TitleUz = command.Title.Uz,
                TitleRu = command.Title.Ru,
                TitleEn = command.Title.En,
                DescriptionOz = command.Description.Oz,
                DescriptionUz = command.Description.Uz,
                DescriptionRu = command.Description.Ru,
                DescriptionEn = command.Description.En,
                CreatedAt = command.CreatedAt,
                ImageUrl = command.ImageUrl,
                ImageId = command.ImageId,
                SchoolId = command.SchoolId,
                DateTime = command.DateTime,
                ShortDescriptionOz = command.ShortDescription.Oz,
                ShortDescriptionUz = command.ShortDescription.Uz,
                ShortDescriptionRu = command.ShortDescription.Ru,
                ShortDescriptionEn = command.ShortDescription.En,
                DirectionId = command.DirectionId
            };
        }
        public static Direction AsEntity(this UpsertDirection command)
        {
            return new Direction
            {
                Id = command.Id,
                NameOz = command.Name.Oz,
                NameUz = command.Name.Uz,
                NameEn = command.Name.En,
                NameRu = command.Name.Ru,
                DescriptionOz = command.Description.Oz,
                DescriptionUz = command.Description.Uz,
                DescriptionRu = command.Description.Ru,
                DescriptionEn = command.Description.En,
                SchoolId = command.SchoolId,
                ImageId = command.ImageId,
                ImageUrl = command.ImageUrl
            };
        }
        public static TopStudent AsEntity(this UpsertTopStudent command)
        {
            return new TopStudent
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                MiddleName = command.MiddleName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                PositionUz = command.Position.Uz,
                PositionEn = command.Position.En,
                PositionOz = command.Position.Oz,
                PositionRu = command.Position.Ru,
                SchoolId = command.SchoolId,
                ImageUrl = command.ImageUrl,
                ImageId = command.ImageId
            };
        }
        public static FAQ AsEntity(this UpsertFAQ command)
        {
            return new FAQ
            {
                Id = command.Id,
                TitleOz = command.Title.Oz,
                TitleUz = command.Title.Uz,
                TitleRu = command.Title.Ru,
                TitleEn = command.Title.En,
                DescriptionOz = command.Description.Oz,
                DescriptionUz = command.Description.Uz,
                DescriptionRu = command.Description.Ru,
                DescriptionEn = command.Description.En,
                SchoolId = command.SchoolId
            };
        }
        public static Gallery AsEntity(this UpsertGallery command)
        {
            return new Gallery
            {
                Id = command.Id,
                TitleOz = command.Title.Oz,
                TitleUz = command.Title.Uz,
                TitleRu = command.Title.Ru,
                TitleEn = command.Title.En,
                SchoolId = command.SchoolId
            };
        }
        public static News AsEntity(this UpsertNews command)
        {
            return new News
            {
                Id = command.Id,
                TitleOz = command.Title.Oz,
                TitleUz = command.Title.Uz,
                TitleRu = command.Title.Ru,
                TitleEn = command.Title.En,
                DescriptionOz = command.Description.Oz,
                DescriptionUz = command.Description.Uz,
                DescriptionRu = command.Description.Ru,
                DescriptionEn = command.Description.En,
                CreatedAt = command.CreatedAt,
                SchoolId = command.SchoolId,
                IsPublished = command.IsPublished,
                ImageId = command.ImageId,
                ImageUrl = command.ImageUrl,
                ShortDescriptionOz = command.ShortDescription.Oz,
                ShortDescriptionUz = command.ShortDescription.Uz,
                ShortDescriptionRu = command.ShortDescription.Ru,
                ShortDescriptionEn = command.ShortDescription.En,
            };
        }
        public static Announcement AsEntity(this UpsertAnnouncement command)
        {
            return new Announcement()
            {
                Id = command.Id,
                TitleOz = command.Title.Oz,
                TitleUz = command.Title.Uz,
                TitleRu = command.Title.Ru,
                TitleEn = command.Title.En,
                DescriptionOz = command.Description.Oz,
                DescriptionUz = command.Description.Uz,
                DescriptionRu = command.Description.Ru,
                DescriptionEn = command.Description.En,
                CreatedAt = command.CreatedAt,
                SchoolId = command.SchoolId,
                IsPublished = command.IsPublished,
                ImageId = command.ImageId,
                ImageUrl = command.ImageUrl,
                ShortDescriptionOz = command.ShortDescription.Oz,
                ShortDescriptionUz = command.ShortDescription.Uz,
                ShortDescriptionRu = command.ShortDescription.Ru,
                ShortDescriptionEn = command.ShortDescription.En,
            };
        }

        public static Page AsEntity(this UpsertPage command)
        {
            return new Page()
            {
                Id = command.Id,
                Url = command.Url,
                NameOz = command.Name.Oz,
                NameUz = command.Name.Uz,
                NameRu = command.Name.Ru,
                NameEn = command.Name.En,
                ContentOz = command.Content.Oz,
                ContentUz = command.Content.Uz,
                ContentRu = command.Content.Ru,
                ContentEn = command.Content.En,
                SchoolId = command.SchoolId
            };
        }

        public static ContactUs AsEntity(this UpsertContactUs command)
        {
            return new ContactUs()
            {
                Id = command.Id,
                Firstname = command.Firstname,
                Lastname = command.Lastname,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Message = command.PhoneNumber
            };
        }
        public static Banner AsEntity(this UpsertBanner command)
        {
            return new Banner()
            {
                Id = command.Id,
                TitleOz = command.Title.Oz,
                TitleUz = command.Title.Uz,
                TitleRu = command.Title.Ru,
                TitleEn = command.Title.En,
                DescriptionOz = command.Description.Oz,
                DescriptionUz = command.Description.Uz,
                DescriptionRu = command.Description.Ru,
                DescriptionEn = command.Description.En,
                CreatedAt = command.CreatedAt,
                SchoolId = command.SchoolId,
                IsPublished = command.IsPublished,
                ImageId = command.ImageId,
                ImageUrl = command.ImageUrl,
                Link = command.Link,
                ShortDescriptionOz = command.ShortDescription.Oz,
                ShortDescriptionUz = command.ShortDescription.Uz,
                ShortDescriptionRu = command.ShortDescription.Ru,
                ShortDescriptionEn = command.ShortDescription.En,
            };
        }
        public static Employee AsEntity(this UpsertEmployee command)
        {
            return new Employee
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                MiddleName = command.MiddleName,
                Email = command.Email,
                IsHead = command.IsHead,
                PhoneNumber = command.PhoneNumber,
                PositionUz = command.Position.Uz,
                PositionEn = command.Position.En,
                PositionOz = command.Position.Oz,
                PositionRu = command.Position.Ru,
                SchoolId = command.SchoolId,
                ImageUrl = command.ImageUrl,
                ImageId = command.ImageId
            };
        }
        public static Book AsEntity(this UpsertBook command)
        {
            return new Book
            {
                Id = command.Id,
                Name = command.Name,
                Author = command.Author,
                SchoolId = command.SchoolId,
                Description = command.Description,
                Year = command.Year,
                FileId = command.FileId,
                FileUrl = command.FileUrl,
                ImageId = command.ImageId,
                ImageUrl = command.ImageUrl
            };
        }
}