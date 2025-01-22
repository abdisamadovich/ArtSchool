using ArtSchools.App.Globalization;

namespace ArtSchools.Dashboard.Commands
{
    public class UpsertDistrict
    {
        public int Id { get; set; }
        public Language Name { get; set; }
        public int RegionId { get; set; }
    }
}
