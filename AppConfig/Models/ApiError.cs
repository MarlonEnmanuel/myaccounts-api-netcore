using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyAccounts.AppConfig.Models
{
    public class ApiError
    {
        public string Title { get; set; }

        public List<string>? Errors { get; set; } = null;

        public List<FieldError>? Fields { get; set; } = null;

        public ApiError(string title)
        {
            Title = title;
        }

        public ApiError(string title, string error)
        {
            Title = title;
            Errors = new List<string> { error };
        }

        public ApiError(string title, params string[] errors)
        {
            Title = title;
            Errors = errors.ToList();
        }

        public ApiError(string title, ModelStateDictionary modelState)
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

        public ApiError(string title, IEnumerable<ValidationFailure> validations)
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
            Field = fieldToCamel(field);
            Errors = errors;
        }

        private string fieldToCamel(string field)
        {
            return string.Join(".", field.Split(".").Select(s => toCamelCase(s)).ToArray());
        }

        private string toCamelCase(string val)
        {
            var strArray = val.ToCharArray().Select(e => e.ToString()).ToArray();
            if (strArray.Length > 1) strArray[0] = strArray[0].ToLower();
            return string.Join("", strArray);
        }
    }
}
