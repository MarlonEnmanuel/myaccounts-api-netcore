using MyAccounts.Api.Database.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Api.Database.Models
{
    public class Person : IIdentity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsShared { get; set; }

        // foreigns

        public User? User { get; set; }

        public List<Card>? Cards { get; set; }

        public List<PaymentSplit>? PaymentSplits { get; set; }
    }
}
