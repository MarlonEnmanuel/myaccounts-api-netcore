using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Database.Interfaces;
using MyAccounts.Api.Modules.Shared.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Api.Database.Models
{
    public class Card : IIdentity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Nullable]
        public int? CutDay { get; set; }

        [Nullable]
        public int? PaymentDay { get; set; }

        // foreigns

        public Person? Person { get; set; }

        public List<Payment>? Payments { get; set; }

        // others

        public string TypeName => Type.GetDescription();
    }
}