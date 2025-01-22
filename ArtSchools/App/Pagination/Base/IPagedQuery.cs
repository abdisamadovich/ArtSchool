namespace ArtSchools.App.Pagination.Base;

public interface IPagedQuery
{
    public int Page { get; }
    public int Results { get; }
    public string OrderBy { get; }
    public string SortOrder { get; }
}