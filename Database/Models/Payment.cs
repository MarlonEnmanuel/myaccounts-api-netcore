using MyAccounts.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAccounts.Database.Models
{
    public abstract class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string Detail { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public int CardId { get; set; }

        // FOREIGNS

        [ForeignKey(nameof(CardId))]
        public virtual Card Card { get; set; }

        public virtual ICollection<PaymentSplit> PaymentSplits { get; set; } = Array.Empty<PaymentSplit>();

        // OTHERS

        public abstract PaymentType Type { get; }

        public decimal Amount => PaymentSplits.Aggregate(0m, (sum, split) => sum + split.Amount);
    }

    public class DebitPayment : Payment
    {
        [Nullable]
        public int? CreditPaymentId { get; set; }

        // FOREIGNS

        [ForeignKey(nameof(CardId))]
        public virtual DebitCard Card { get; set; }

        [ForeignKey(nameof(CreditPaymentId))]
        public CreditPayment? CreditPayment { get; set; }

        // OTHERS

        public override PaymentType Type => PaymentType.Debit;
    }

    public class CreditPayment : Payment
    {
        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int NumberOfFees { get; set; }

        // FOREIGNS

        [ForeignKey(nameof(CardId))]
        public virtual CreditCard Card { get; set; }

        public virtual ICollection<DebitPayment> DebitPayments { get; set; } = Array.Empty<DebitPayment>();

        // OTHERS

        public override PaymentType Type => PaymentType.Credit;
    }
}