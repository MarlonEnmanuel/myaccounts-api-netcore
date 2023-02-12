using FluentValidation;

namespace MyAccounts.Modules.Shared.Validation
{
    public interface IValidatorService
    {
        public TValidator GetDtoValidator<TValidator>() where TValidator : IValidator;
    }

    public class ValidatorService : IValidatorService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TValidator GetDtoValidator<TValidator>() where TValidator : IValidator
        {
            return _serviceProvider.GetService<TValidator>()
                                ?? throw new Exception($"The validator {typeof(TValidator).Name} not found");
        }
    }
}
