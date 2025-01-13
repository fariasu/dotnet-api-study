using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Communication.DTOs.Response;
using TaskManager.Exception.ExceptionsBase;

namespace TaskManager.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private const string UNKNOWN_ERROR_MESSAGE = "Unknown Error";
    
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is TaskManagerException)
            HandleProjectException(context);
        else
            ThrowUnknownError(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = (TaskManagerException)context.Exception;
        var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(UNKNOWN_ERROR_MESSAGE);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}