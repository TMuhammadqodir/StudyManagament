namespace StudyManagement.Service.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string massage) : base(massage)
    { }

    public AlreadyExistException(string massage, Exception exception) : base(massage, exception)
    { }

    public int StatusCode { get; set; } = 403;
}
