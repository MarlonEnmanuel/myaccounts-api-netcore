using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyAccounts.AppConfig.Models
{
    public class AppErrorResult
    {
        public const string ValidationTitle = "Algunos datos son incorrectos";

        public string Title { get; set; }

        public List<string> Errors { get; set; }

        public List<FieldError> Fields { get; set; }

        public AppErrorResult(ModelStateDictionary modelState)
        {
            Title = ValidationTitle;

            var getErrors = (string key) => modelState[key]!.Errors.Select(e => e.ErrorMessage).ToList();

            Errors = modelState.Keys
                        .Where(key => key == string.Empty)
                        .SelectMany(key => getErrors(key))
                        .ToList();

            Fields = modelState.Keys
                        .Where(key => key != string.Empty)
                        .Select(key => new FieldError(key, getErrors(key)))
                        .ToList();
        }

        public AppErrorResult(IEnumerable<ValidationFailure> validations)
        {
            Title = ValidationTitle;

            Errors = new List<string>();

            var field = validations.Select(x => x.PropertyName).Distinct().ToList();

            Fields = validations
                        .Select(x => x.PropertyName)
                        .Distinct()
                        .Select(field =>
                        {
                            var e = validations.Where(v => v.PropertyName == field)
                                                .Select(v => v.ErrorMessage)
                                                .ToList();
                            return new FieldError(field, e);
                        })
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
