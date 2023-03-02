using MyAccounts.Database.Enums;

namespace MyAccounts.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int PersonId { get; set; }

        public PaymentType Type { get; set; }

        public int? CutDay { get; set; }

        public int? PaymentDay { get; set; }

        public bool IsDebit { get; set; }

        public bool IsCredit { get; set; }
    }
}
