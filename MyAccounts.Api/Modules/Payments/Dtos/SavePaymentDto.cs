using MyAccounts.Api.Database.Enums;

namespace MyAccounts.Api.Modules.Payments.Dtos
{
    public class SavePaymentDto
    {
        public int Id { get; set; } = 0;

        public string Date { get; set; } = string.Empty;

        public PaymentType Type { get; set; }

        public int CardId { get; set; }

        public string Detail { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public int? CreditFees { get; set; }

        public decimal? CreditAmount { get; set; }

        public List<SavePaymentSplitDto> PaymentSplits { get; set; } = new();
    }

    public class SavePaymentSplitDto
    {
        public int PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}
