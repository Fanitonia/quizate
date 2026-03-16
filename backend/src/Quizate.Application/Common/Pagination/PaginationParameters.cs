using Microsoft.AspNetCore.Mvc;

namespace Quizate.Application.Common.Pagination;

public class PaginationParameters
{
    private const int MAX_PAGE_SIZE = 50;
    private int _pageSize = 50;

    [BindProperty(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;

    [BindProperty(Name = "pageSize")]
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value; }
    }
}
