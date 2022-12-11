using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Database.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        // FOREIGNS

        public virtual ICollection<User> Users { get; set; } = Array.Empty<User>();

        public virtual ICollection<Card> Cards { get; set; } = Array.Empty<Card>();

        public virtual ICollection<PaymentSplit> PaymentSplits { get; set; } = Array.Empty<PaymentSplit>();
    }
}
