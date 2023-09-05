using StudyManagement.Domain.Configrations;

namespace StudyManagement.Service.Extentions;

public static class CollectionExtension
{
    public static IEnumerable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams @params)
    {
        var source =values.Skip((@params.PageIndex-1)*@params.PageSize).Take(@params.PageSize);
        return source;
    }
}
