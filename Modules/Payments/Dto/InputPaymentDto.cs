using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Modules.Payments.Dto
{
    public class InputPaymentDto
    {
        [Required]
        public int Id { get; set; } = 0;

        [Required]
        public int CardId { get; set; }

        [Required]
        public string Date { get; set; } = null!;

        [Required]
        public string Detail { get; set; } = null!;

        [Required(AllowEmptyStrings = true)]
        public string Comment { get; set; } = null!;

        public int? CreditFees { get; set; }

        public decimal? CreditAmount { get; set; }

        [Required]
        public IList<InputPaymentSplitDto> PaymentSplits { get; set; } = null!;
    }

    public class InputPaymentSplitDto
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
