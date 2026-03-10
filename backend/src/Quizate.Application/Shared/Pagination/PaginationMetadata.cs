namespace Quizate.Application.Shared.Pagination;

public class PaginationMetadata
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1 && CurrentPage <= TotalPages + 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PaginationMetadata(int pageSize, int pageNumber, int totalCount)
    {
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalCount = totalCount;
        TotalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
