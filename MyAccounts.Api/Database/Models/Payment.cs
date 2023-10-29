using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Database.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyAccounts.Api.Database.Models
{
    public class Payment : IIdentity, IAuditable
    {
        #region Attributes

        [Key]
        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string Detail { get; set; }

        [Required]
        public string Comment { get; set; }

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

        #endregion

        #region Foreigns

        [JsonIgnore]
        public virtual Card Card { get; set; } = default!;

        [JsonIgnore]
        public virtual ICollection<PaymentSplit> PaymentSplits { get; set; } = default!;

        #endregion

        #region Others

        public decimal Amount => GetAmount();

        public Payment(int cardId, PaymentType type, DateOnly date, string detail, string comment, int? creditFees, decimal? creditAmount)
        {
            CardId = cardId;
            Type = type;
            Date = date;
            Detail = detail ?? throw new ArgumentNullException(nameof(detail));
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            CreditFees = creditFees;
            CreditAmount = creditAmount;
        }

        private decimal GetAmount ()
        {
            var sum = PaymentSplits?.Aggregate(0m, (sum, split) => sum + split.Amount);
            return sum ?? throw new ArgumentNullException(nameof(PaymentSplits));
        }

        #endregion
    }
}