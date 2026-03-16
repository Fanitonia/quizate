namespace Quizate.Application.Common.Pagination;

public class PaginatedList<TData>
{
    public PaginationMetadata PaginationMetadata { get; private set; }
    public ICollection<TData> Records { get; private set; }

    public PaginatedList(PaginationMetadata paginationMetadata, ICollection<TData> records)
    {
        PaginationMetadata = paginationMetadata;
        Records = records;
    }
}
