using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MoviesRate.Exception.Exceptions;
using MoviesRate.Communication.Response;
using MoviesRate.Exception;

namespace MoviesRate.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MoviesRateException moviesRateException)
            HandleProjectException(moviesRateException, context);
        else
            ThrowUnknowException(context);
    }

    private static void HandleProjectException(MoviesRateException moviesRateException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)moviesRateException.GetStatusCode();
        context.Result = new ObjectResult(new ErrorResponse(moviesRateException.GetErrorMessages()));
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ErrorResponse(MessagesException.UNKNOWN_ERROR));
    }
}