using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAccounts.AppConfig.Models;

namespace MyAccounts.AppConfig.Filters
{
    public class AppValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new AppErrorResult(context.ModelState);
                context.Result = new UnprocessableEntityObjectResult(result);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) {}
    }
}
