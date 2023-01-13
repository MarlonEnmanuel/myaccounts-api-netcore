using Microsoft.VisualBasic;
using MyAccounts.Database.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyAccounts.Database.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // FOREIGNS

        public virtual User? User { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new Collection<Card>();

        public virtual ICollection<PaymentSplit> PaymentSplits { get; set; } = new Collection<PaymentSplit>();

        // OTROS

        public ICollection<DebitCard> DebitCards => Cards.Where(c => c.Type.IsDebit()).Select(c => (DebitCard)c).ToList();
        public ICollection<CreditCard> CreditCards => Cards.Where(c => c.Type.IsCredit()).Select(c => (CreditCard)c).ToList();
    }
}
