using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace MyAccounts.Api.Database.Models
{
    [PrimaryKey(nameof(PaymentId), nameof(PersonId))]
    public class PaymentSplit
    {
        #region Attributes

        [Required]
        public int PaymentId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        #endregion

        #region Foreigns

        [JsonIgnore]
        public virtual Payment Payment { get; set; } = default!;

        [JsonIgnore]
        public virtual Person Person { get; set; } = default!;

        #endregion

        #region Others

        public PaymentSplit (int personId, decimal amount)
        {
            PersonId = personId;
            Amount = amount;
        }

        #endregion
    }
}