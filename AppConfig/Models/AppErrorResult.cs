using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace MyAccounts.AppConfig.Models
{
    public class AppErrorResult
    {
        public const string ValidationTitle = "Algunos datos son incorrectos";

        public string Title { get; set; }

        public List<string> Errors { get; set; }

        public List<FieldError> Fields { get; set; }

        public AppErrorResult(string title, ModelStateDictionary modelState)
        {
            Title = title;

            var getErrors = (string key) => modelState[key]!.Errors.Select(e => e.ErrorMessage).ToList();

            Errors = modelState.Keys
                        .Where(key => key.IsNullOrEmpty())
                        .SelectMany(key => getErrors(key))
                        .ToList();

            Fields = modelState.Keys
                        .Where(key => !key.IsNullOrEmpty())
                        .Select(key => new FieldError(key, getErrors(key)))
                        .ToList();
        }
    }

    public class FieldError
    {
        public string Field { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public FieldError(string field, List<string> errors)
        {
            Field = field;
            Errors = errors;
        }
    }
}
