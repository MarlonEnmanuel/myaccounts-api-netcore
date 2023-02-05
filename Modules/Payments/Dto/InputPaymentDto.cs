using MyAccounts.Database.Enums;

namespace MyAccounts.Modules.Payments.Dto
{
    public class InputPaymentDto
    {
        public int Id { get; set; } = 0;

        public int CardId { get; set; }

        public PaymentType Type { get; set; }

        public string Date { get; set; } = null!;

        public string Detail { get; set; } = null!;

        public string Comment { get; set; } = null!;

        public int? CreditFees { get; set; }

        public decimal? CreditAmount { get; set; }

        public IList<InputPaymentSplitDto> PaymentSplits { get; set; } = null!;
    }

    public class InputPaymentSplitDto
    {
        public int PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}
