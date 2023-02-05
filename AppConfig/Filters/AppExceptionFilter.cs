using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAccounts.AppConfig.Models;

namespace MyAccounts.AppConfig.Filters
{
    public class AppExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (ValidationException exception)
            {
                var errorResult = new AppErrorResult(exception.Errors);
                context.Result = new UnprocessableEntityObjectResult(errorResult);
            }
            catch { }
        }
    }
}
