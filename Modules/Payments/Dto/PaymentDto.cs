using MyAccounts.Database.Enums;

namespace MyAccounts.Modules.Payments.Dto
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public PaymentType Type { get; set; }

        public DateOnly Date { get; set; }

        public string Detail { get; set; } = null!;

        public string Comment { get; set; } = null!;

        public int? CreditFees { get; set; }

        public decimal? CreditAmount { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<PaymentSplitDto> PaymentSplits { get; set; } = Array.Empty<PaymentSplitDto>();
    }

    public class PaymentSplitDto
    {
        public int PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}
