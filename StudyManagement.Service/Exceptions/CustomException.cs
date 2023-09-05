namespace StudyManagement.Service.Exceptions;

public class CustomException : Exception
{
    public CustomException(string massage) : base(massage)
    { }

    public CustomException(string massage, Exception exception) : base(massage, exception)
    { }

    public int StatusCode { get; set; } = 422;
}
