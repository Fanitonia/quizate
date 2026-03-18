namespace Quizate.Application.Common.Pagination;

public class PaginationMetadata
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1 && CurrentPage <= TotalPages + 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PaginationMetadata(PaginationParameters pagination, int totalCount)
    {
        PageSize = pagination.PageSize;
        CurrentPage = pagination.PageNumber;
        TotalCount = totalCount;
        TotalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pagination.PageSize);
    }
}
