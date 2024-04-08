using System.ComponentModel;

namespace MyAccounts.Api.Database.Enums
{
    public enum PaymentType
    {
        [Description("Débito")] Debit = 1,
        [Description("Crédito")] Credit = 2,
    }
}