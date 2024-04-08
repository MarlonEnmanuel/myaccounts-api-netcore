using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Database.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MyAccounts.Api.Database.Models
{
    public class Payment : IIdentity, IAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string Detail { get; set; } = string.Empty;

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public decimal PaymentAmount { get; set; }

        public int? Installments { get; set; }

        public decimal? InstallmentAmount { get; set; }

        // IAuditable

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        // foreigns

        public Card? Card { get; set; }

        public List<PaymentSplit>? PaymentSplits { get; set; }
    }
}