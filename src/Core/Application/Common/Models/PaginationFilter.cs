using System.Text.Json.Serialization;

namespace Application.Common.Models;

public class PaginationFilter : BasicPaginationFilter
{

}

public class BasicPaginationFilter : IPagination, IOrderBy
{
    public string? Keyword { get; set; }
    private int _pageNumber;
    private int _pageSize;

    public int PageNumber
    {
        get { return _pageNumber <= 0 ? 1 : _pageNumber; }
        set { _pageNumber = value; }
    }

    public int PageSize
    {
        get { return _pageSize <= 0 ? 10 : _pageSize; }
        set { _pageSize = value == 0 ? int.MaxValue : value; }
    }

    public string[]? OrderBy { get; set; }

    public bool HasOrderBy() => OrderBy?.Any() is true;

    [JsonIgnore]
    public bool ForceUseCustomOrderBy { get; set; }

    [JsonIgnore]
    public bool IgnorePagination { get; set; }
}