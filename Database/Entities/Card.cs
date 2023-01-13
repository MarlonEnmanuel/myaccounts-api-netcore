﻿using MyAccounts.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAccounts.Database.Entities
{
    public abstract class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PersonId { get; set; }

        // FOREIGNS

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }

        // OTHERS

        public abstract PaymentType Type { get; }
    }

    public class DebitCard : Card
    {
        public virtual ICollection<DebitPayment> DebitPayments { get; set; } = Array.Empty<DebitPayment>();

        // OTHERS

        public override PaymentType Type => PaymentType.Debit;
    }

    public class CreditCard : Card
    {
        [Required]
        public int CutDay { get; set; }

        [Required]
        public int PaymentDay { get; set; }

        public virtual ICollection<CreditPayment> CreditPayments { get; set; } = Array.Empty<CreditPayment>();

        // OTHERS

        public override PaymentType Type => PaymentType.Credit;
    }
}