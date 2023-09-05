using StudyManagement.Service.Exceptions;
using StudyManagement.WebApi.Models;

namespace StudyManagement.WebApi.Exceptions;

public class ExceptionHandlarMiddleware
{
    private readonly RequestDelegate request;

    private readonly ILogger<ExceptionHandlarMiddleware> logger;

    public ExceptionHandlarMiddleware(RequestDelegate request, ILogger<ExceptionHandlarMiddleware> logger)
    {
        this.request = request;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await request.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = context.Response.StatusCode,
                Massage = ex.Message,
            });
        }
        catch (AlreadyExistException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = context.Response.StatusCode,
                Massage = ex.Message,
            });
        }
        catch (CustomException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = context.Response.StatusCode,
                Massage = ex.Message,
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            logger.LogError(ex.Message);
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = context.Response.StatusCode,
                Massage = ex.Message,
            });
        }
    }
}
