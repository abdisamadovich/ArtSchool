using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSchools.Entities;

[Table("schools", Schema = "schools")]
public class School : IIdentifiable<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string DomainId { get; set; }
    public int Number { get; set; }
    public string Type { get; set; }

    public string NameOz { get; set; }
    public string NameUz { get; set; }
    public string NameRu { get; set; }
    public string NameEn { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public string SiteLink { get; set; }
    
    public TimeSpan? WorkingHoursStart { get; set; }
    public TimeSpan? WorkingHoursEnd { get; set; }
    public DayOfWeek? WorkingDaysStart { get; set; }
    public DayOfWeek? WorkingDaysEnd { get; set; }
    public TimeSpan? LunchStart { get; set; }
    public TimeSpan? LunchEnd { get; set; }

    public District District { get; set; }
    public int DistrictId { get; set; }
    public Region Region { get; set; }
    public int RegionId { get; set; }

    public string TelegramLink { get; set; }
    public string InstagramLink { get; set; }
    public string FacebookLink { get; set; }
    public string YoutubeLink { get; set; }

    public string DescriptionOz { get; set; }
    public string DescriptionUz { get; set; }
    public string DescriptionRu { get; set; }
    public string DescriptionEn { get; set; }
    public string ShortDescriptionOz { get; set; }
    public string ShortDescriptionUz { get; set; }
    public string ShortDescriptionRu { get; set; }
    public string ShortDescriptionEn { get; set; }

    public string Map { get; set; }
    [ForeignKey(nameof(ImageId))]
    public File Image { get; set; }
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
    
    public IList<Book> Books { get; set; }
    public IList<Direction> Directions { get; set; }
    public IList<Employee> Employees { get; set; }
    public IList<Event> Events { get; set; }
    public IList<FAQ> Faqs { get; set; }
    public IList<Gallery> Galleries { get; set; }
    public IList<News> News { get; set; }
    public IList<TopStudent> TopStudents { get; set; }
    public IList<Vacancy> Vacancies { get; set; }

    public bool IsDeleted { get; set; }
}