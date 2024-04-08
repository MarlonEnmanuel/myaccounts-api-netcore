using FluentValidation;
using MyAccounts.Api.Database;
using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Modules.Payments.Dtos;
using MyAccounts.Api.Modules.Shared.Extensions;

namespace MyAccounts.Api.Modules.Payments.Validators
{
    public class SavePaymentDtoValidator : AbstractValidator<SavePaymentDto>
    {
        private readonly MyAccountsContext _context;

        public SavePaymentDtoValidator(MyAccountsContext context)
        {
            _context = context;
            
            RuleFor(x => x.CardId)
                .GreaterThan(0)
                .AppExistsIdAsync(_context.Cards);

            RuleFor(x => x.Type)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.Date)
                .NotEmpty();

            RuleFor(x => x.Detail)
                .NotEmpty();

            RuleFor(x => x.Comment)
                .NotNull();

            RuleFor(x => x.PaymentAmount)
                .GreaterThan(0);

            RuleFor(x => x.Installments)
                .Null()
                .When(x => x.Type == PaymentType.Debit);

            RuleFor(x => x.Installments)
                .GreaterThan(0)
                .When(x => x.Type == PaymentType.Credit);

            RuleFor(x => x.InstallmentAmount)
                .Null()
                .When(x => x.Type == PaymentType.Debit);

            RuleFor(x => x.InstallmentAmount)
                .GreaterThan(0)
                .When(x => x.Type == PaymentType.Credit);

            RuleFor(x => x.PaymentSplits)
                .NotEmpty()
                .AppDisctinctValues(x => x.PersonId)
                .AppExistsIdsAsync(_context.Persons, x => x.PersonId);

            RuleForEach(x => x.PaymentSplits)
                .ChildRules(split =>
                {
                    split.RuleFor(x => x.PersonId)
                            .GreaterThan(0)
                            .AppExistsIdAsync(_context.Persons);

                    split.RuleFor(x => x.Amount)
                            .GreaterThan(0);
                });
        }
    }
}
