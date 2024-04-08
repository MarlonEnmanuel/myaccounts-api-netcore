using MyAccounts.Api.Database.Enums;

namespace MyAccounts.Api.Modules.Payments.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public PaymentType Type { get; set; }

        public DateOnly Date { get; set; }

        public string Detail { get; set; } = null!;

        public string Comment { get; set; } = null!;

        public decimal PaymentAmount { get; set; }

        public int? Installments { get; set; }

        public decimal? InstallmentAmount { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public List<PaymentSplitDto> PaymentSplits { get; set; } = default!;
    }

    public class PaymentSplitDto
    {
        public int PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}
