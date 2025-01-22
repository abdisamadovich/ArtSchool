using ArtSchools.App.Globalization;
using ArtSchools.Entities;

namespace ArtSchools.Dashboard.Commands;

public class UpsertSchool
{
    public int Id { get; set; }
    public string DomainId { get; set; }
    public int Number { get; set; }
    public Language Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public string SiteLink { get; set; }
    
    public TimeSpan? WorkingHoursStart { get; set; }
    public TimeSpan? WorkingHoursEnd { get; set; }
    public DayOfWeek? WorkingDaysStart { get; set; }
    public DayOfWeek? WorkingDaysEnd { get; set; }
    public TimeSpan? LunchStart { get; set; }
    public TimeSpan? LunchEnd { get; set; }

    public int DistrictId { get; set; }
    public int RegionId { get; set; }
    
    public string TelegramLink { get; set; }
    public string InstagramLink { get; set; }
    public string FacebookLink { get; set; }
    public string YoutubeLink { get; set; }

    public Language Description { get; set; }
    public Language ShortDescription { get; set; }

    public string Map { get; set; }
    public int? ImageId { get; set; }
    public string ImageUrl { get; set; }
    public string VideoLink { get; set; }
    public int ClassCount { get; set; }
    public int TeacherCount { get; set; }
    public int SpecialClassCount { get; set; }
    public int StudentCount { get; set; }
    public int Years { get; set; }

    public int AgeLvlOne { get; set; }
    public int AgeLvlTwo { get; set; }
    public int AgeLvlThree { get; set; }
    public int AgeLvlFour { get; set; }
    public int AgeLvlFive { get; set; }
    public int AgeLvlSix { get; set; }
    public int BoyCount { get; set; }
    public int GirlCount { get; set; }
    public string PhoneNumber { get; set; }
}