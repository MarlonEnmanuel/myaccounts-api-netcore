using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Database.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Api.Database.Models
{
    public class Payment : IIdentity, IAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string Detail { get; set; } = string.Empty;

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Nullable]
        public int? CreditFees { get; set; }

        [Nullable]
        public decimal? CreditAmount { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        // foreigns

        public Card? Card { get; set; }

        public List<PaymentSplit>? PaymentSplits { get; set; }

        // others

        public decimal Amount => GetAmount();

        private decimal GetAmount ()
        {
            var sum = PaymentSplits?.Aggregate(0m, (sum, split) => sum + split.Amount);
            return sum ?? throw new ArgumentNullException(nameof(PaymentSplits));
        }
    }
}