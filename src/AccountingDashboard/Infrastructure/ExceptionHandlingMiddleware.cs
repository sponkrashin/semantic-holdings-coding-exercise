namespace AccountingDashboard.Infrastructure;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

public class ExceptionHandlingMiddleware(IProblemDetailsService problemDetailsService, IWebHostEnvironment environment, ILogger<ExceptionHandlingMiddleware> logger)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during the request handling");
            await this.HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var statusCode = this.GetStatusCode(exception);

        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Detail = environment.IsDevelopment() ? exception.Message : string.Empty,
                Status = statusCode,
            }
        });
    }

    private int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            ArgumentException => StatusCodes.Status422UnprocessableEntity,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
