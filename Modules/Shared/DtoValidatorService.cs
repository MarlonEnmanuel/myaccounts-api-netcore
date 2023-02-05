using FluentValidation;
using FluentValidation.Results;

namespace MyAccounts.Modules.Shared
{
    public interface IDtoValidatorService
    {
        public ValidationResult Validate<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>;

        public void ValidateAndThrow<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>;

        public Task<ValidationResult> ValidateAsync<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>;

        public Task ValidateAndThrowAsync<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>;
    }

    public class DtoValidator : IDtoValidatorService
    {
        private readonly IServiceProvider _serviceProvider;

        public DtoValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ValidationResult Validate<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>
        {
            var validator = GetValidator<TValidator>();
            return validator.Validate(dto);
        }

        public void ValidateAndThrow<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>
        {
            var validator = GetValidator<TValidator>();
            validator.ValidateAndThrow(dto);
        }

        public Task<ValidationResult> ValidateAsync<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>
        {
            var validator = GetValidator<TValidator>();
            return validator.ValidateAsync(dto);
        }

        public Task ValidateAndThrowAsync<TValidator, TDto>(TDto dto) where TValidator : IValidator<TDto>
        {
            var validator = GetValidator<TValidator>();
            return validator.ValidateAndThrowAsync(dto);
        }

        private TValidator GetValidator<TValidator>() where TValidator : IValidator
        {
            return _serviceProvider.GetService<TValidator>()
                    ?? throw new Exception($"The validator {typeof(TValidator).Name} not found");
        }
    }
}
