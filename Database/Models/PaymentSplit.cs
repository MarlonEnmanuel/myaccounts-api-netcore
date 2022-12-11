using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyAccounts.Database.Models
{
    [PrimaryKey(nameof(PaymentId), nameof(PersonId))]
    public class PaymentSplit
    {
        [Required]
        public int PaymentId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        // FOREIGNS

        [ForeignKey(nameof(PaymentId))]
        public virtual Payment Payment { get; set; } = null!;

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; } = null!;
    }
}