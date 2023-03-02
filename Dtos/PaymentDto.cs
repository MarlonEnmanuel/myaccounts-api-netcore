using MyAccounts.Database.Enums;

namespace MyAccounts.Dtos
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

        public IList<PaymentSplitDto> PaymentSplits { get; set; } = null!;

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }

    public class PaymentSplitDto
    {
        public int PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}
