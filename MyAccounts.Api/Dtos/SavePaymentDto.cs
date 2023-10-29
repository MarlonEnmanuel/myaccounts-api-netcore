using MyAccounts.Api.Database.Enums;

namespace MyAccounts.Api.Dtos
{
    public class SavePaymentDto
    {
        public int Id { get; set; } = 0;

        public string Date { get; set; } = null!;

        public PaymentType Type { get; set; }

        public int CardId { get; set; }

        public string Detail { get; set; } = null!;

        public string Comment { get; set; } = null!;

        public int? CreditFees { get; set; }

        public decimal? CreditAmount { get; set; }

        public IList<SavePaymentSplitDto> PaymentSplits { get; set; } = null!;
    }

    public class SavePaymentSplitDto
    {
        public int PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}
