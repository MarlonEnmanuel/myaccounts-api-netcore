using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyAccounts.AppConfig.Models
{
    public class AppErrorResult
    {
        public string Title { get; set; }

        public List<string>? Errors { get; set; } = null;

        public List<FieldError>? Fields { get; set; } = null;

        public AppErrorResult(string title)
        {
            Title = title;
        }

        public AppErrorResult(string title, string error)
        {
            Title = title;
            Errors = new List<string> { error };
        }

        public AppErrorResult(string title, params string[] errors)
        {
            Title = title;
            Errors = errors.ToList();
        }

        public AppErrorResult(string title, ModelStateDictionary modelState)
        {
            Title = title;

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

        public AppErrorResult(string title, IEnumerable<ValidationFailure> validations)
        {
            Title = title;

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
        public List<string> Errors { get; set; }

        public FieldError(string field, List<string> errors)
        {
            Field = field;
            Errors = errors;
        }
    }
}
