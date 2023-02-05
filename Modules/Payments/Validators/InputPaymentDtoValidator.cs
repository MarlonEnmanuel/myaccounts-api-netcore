using FluentValidation;
using MyAccounts.Database.Enums;
using MyAccounts.Modules.Payments.Dto;

namespace MyAccounts.Modules.Payments.Validators
{
    public class InputPaymentDtoValidator : AbstractValidator<InputPaymentDto>
    {
        public InputPaymentDtoValidator()
        {
            RuleFor(x => x.CardId)
                .NotNull()
                .NotEmpty()
                ;
            RuleFor(x => x.Type)
                .NotNull()
                .NotEmpty()
                .IsInEnum()
                ;
            RuleFor(x => x.Date)
                .NotNull()
                .NotEmpty()
                ;
            RuleFor(x => x.Detail)
                .NotNull()
                .NotEmpty()
                ;
            RuleFor(x => x.Comment)
                .NotNull()
                ;
            RuleFor(x => x.CreditFees)
                .NotNull().When(x => x.Type == PaymentType.Credit)
                .NotEmpty().When(x => x.Type == PaymentType.Credit)
                ;
            RuleFor(x => x.CreditAmount)
                .NotNull().When(x => x.Type == PaymentType.Credit)
                .NotEmpty().When(x => x.Type == PaymentType.Credit)
                ;
            RuleFor(x => x.PaymentSplits)
                .NotNull()
                .NotEmpty()
                ;
        }
    }
}
