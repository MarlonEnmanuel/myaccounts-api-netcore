using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAccounts.AppConfig.Exceptions;
using MyAccounts.AppConfig.Models;

namespace MyAccounts.AppConfig.Filters
{
    public class AppExceptionFilter : IExceptionFilter
    {
        private const string VALIDATION_TITLE = "Algunos datos son incorrectos";
        private const string APP_ERROR_TITLE = "Ocurrió un problema";
        private const string SERVER_ERROR_TITLE = "Ocurrió un error inesperado";

        public void OnException(ExceptionContext context)
        {
            var exceptionString = context.Exception.ToString();
            try
            {
                throw context.Exception;
            }
            catch (ValidationException exception)
            {
                var errorResult = new AppErrorResult(VALIDATION_TITLE, exception.Errors);
                context.Result = new UnprocessableEntityObjectResult(errorResult);
            }
            catch (AppClientException exception)
            {
                var errorResult = new AppErrorResult(APP_ERROR_TITLE, exception.Message);
                context.Result = new ObjectResult(errorResult) { StatusCode = StatusCodes.Status400BadRequest };
            }
            catch (AppErrorException exception)
            {
                var errorResult = new AppErrorResult(APP_ERROR_TITLE, exception.Message);
                context.Result = new ObjectResult(errorResult) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            catch
            {
                var errorResult = new AppErrorResult(SERVER_ERROR_TITLE, exceptionString);
                context.Result = new ObjectResult(errorResult) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
