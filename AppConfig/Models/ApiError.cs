using FluentValidation.Results;

namespace MyAccounts.AppConfig.Models
{
    public class ApiError
    {
        public string Title { get; set; }

        public string[]? Errors { get; set; } = null;

        public IDictionary<string, string[]>? Fields2 { get; set; } = null;

        public ApiError(string title)
        {
            Title = title;
        }

        public ApiError(string title, string error)
        {
            Title = title;
            Errors = new[] { error };
        }

        public ApiError(string title, params string[] errors)
        {
            Title = title;
            Errors = errors;
        }

        public ApiError(string title, IEnumerable<ValidationFailure> validations)
        {
            Title = title;

            var field = validations.Select(x => x.PropertyName).Distinct().ToList();

            Fields2 = validations
                        .Select(x => x.PropertyName)
                        .Distinct()
                        .Select(prop =>
                        {
                            var field = ToCamelCase(prop);
                            var errors = validations.Where(v => v.PropertyName == prop)
                                                    .Select(v => v.ErrorMessage)
                                                    .ToArray();
                            return new { field, errors };
                        })
                        .ToDictionary(o => o.field, o => o.errors);
        }

        private string ToCamelCase(string source)
        {
            return string.IsNullOrEmpty(source) ? source : char.ToLower(source[0]) + source.Substring(1);
        }
    }
}
