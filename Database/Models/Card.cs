using MyAccounts.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyAccounts.Database.Models
{
    public class Card
    {
        #region Attributes

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Nullable]
        public int? CutDay { get; set; }

        [Nullable]
        public int? PaymentDay { get; set; }

        #endregion

        #region Foreigns

        [JsonIgnore]
        public virtual Person? Person { get; set; }

        [JsonIgnore]
        public virtual ICollection<Payment>? Payments { get; set; }

        #endregion

        #region Others

        public bool IsDebit => Type == PaymentType.Debit;

        public bool IsCredit => Type == PaymentType.Credit;

        public Card (string name, int personId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PersonId = personId;
            Type = PaymentType.Debit;
        }

        public Card (string name, int personId, int cutDay, int paymentDay)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PersonId = personId;
            Type = PaymentType.Credit;
            CutDay = cutDay;
            PaymentDay = paymentDay;
        }

        #endregion
    }
}