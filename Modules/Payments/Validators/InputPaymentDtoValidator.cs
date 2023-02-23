using FluentValidation;
using MyAccounts.Database.Context;
using MyAccounts.Database.Enums;
using MyAccounts.Modules.Payments.Dto;
using MyAccounts.Modules.Security;
using MyAccounts.Modules.Shared.Validation;

namespace MyAccounts.Modules.Payments.Validators
{
    public class InputPaymentDtoValidator : AbstractValidator<InputPaymentDto>
    {
        private readonly MyAccountsContext _context;

        public InputPaymentDtoValidator(MyAccountsContext context)
        {
            _context = context;
            
            RuleFor(x => x.CardId)
                .NotEmpty()
                .AppExistsIdAsync(_context.Cards)
                ;
            RuleFor(x => x.Type)
                .NotEmpty()
                .IsInEnum()
                ;
            RuleFor(x => x.Date)
                .NotEmpty()
                ;
            RuleFor(x => x.Detail)
                .NotEmpty()
                ;
            RuleFor(x => x.Comment)
                .NotNull()
                ;
            RuleFor(x => x.CreditFees)
                .Null()
                .When(x => x.Type.IsDebit())
                ;
            RuleFor(x => x.CreditFees)
                .NotEmpty()
                .When(x => x.Type.IsCredit())
                ;
            RuleFor(x => x.CreditAmount)
                .Null()
                .When(x => x.Type.IsDebit())
                ;
            RuleFor(x => x.CreditAmount)
                .NotEmpty()
                .When(x => x.Type.IsCredit())
                ;
            RuleFor(x => x.PaymentSplits)
                .NotNull()
                .NotEmpty()
                .AppDisctinctValues(x => x.PersonId)
                .AppExistsIdsAsync(_context.Persons, x => x.PersonId)
                ;
            RuleForEach(x => x.PaymentSplits)
                .ChildRules(split =>
                {
                    split.RuleFor(x => x.PersonId)
                            .NotEmpty()
                            .AppExistsIdAsync(_context.Persons)
                            ;
                    split.RuleFor(x => x.Amount)
                            .NotEmpty()
                            .GreaterThan(0)
                            ;
                });
        }
    }
}
