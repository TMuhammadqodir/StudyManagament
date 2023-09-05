namespace StudyManagement.Domain.Configrations;

public class PaginationParams
{
    private const int maxSize = 20;

    private int pageSize;

    public int PageSize
    {
        get { return pageSize == 0 ? maxSize: pageSize; }
        set { pageSize = value > maxSize? maxSize: value; }
    }

    public int PageIndex { get; set; } = 1;
}
