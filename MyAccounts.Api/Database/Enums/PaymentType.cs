using System.ComponentModel;

namespace MyAccounts.Api.Database.Enums
{
    public enum PaymentType
    {
        [Description("Débito")] Debit = 1,
        [Description("Crédito")] Credit = 2,
    }

    public static class PaymentTypeExtension
    {
        public static bool IsDebit(this PaymentType type) => type == PaymentType.Debit;
        public static bool IsCredit(this PaymentType type) => type == PaymentType.Credit;
    }
}