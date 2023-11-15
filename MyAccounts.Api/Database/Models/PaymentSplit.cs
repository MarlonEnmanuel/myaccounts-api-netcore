using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyAccounts.Api.Database.Models
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

        // foreigns

        public Payment? Payment { get; set; }

        public Person? Person { get; set; }
    }
}