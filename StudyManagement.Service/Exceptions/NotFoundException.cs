namespace StudyManagement.Service.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string massage): base(massage)
    {}

    public NotFoundException(string massage, Exception exception) : base(massage, exception)
    {}

    public int StatusCode { get; set; } = 404;
}
