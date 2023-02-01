using MyAccounts.Database.Context;
using MyAccounts.Database.Enums;
using MyAccounts.Database.Models;
using MyAccounts.Modules.Payments.Dto;

namespace MyAccounts.Modules.Payments
{
    public interface IPaymentService
    {
        public Task<Payment> CreatePayment(InputPaymentDto dto);
    }

    public class PaymentService : IPaymentService
    {
        private readonly MyAccountsContext _context;

        public PaymentService(MyAccountsContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayment(InputPaymentDto dto)
        {
            var card = await _context.Cards.FindAsync(dto.CardId) ?? throw new Exception("Tarjeta no existe");
            var newPayment = ToPayment(dto, card.Type);
            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();
            return newPayment;
        }

        private Payment ToPayment(InputPaymentDto dto, PaymentType type)
        {
            var parsedDate = DateOnly.ParseExact(dto.Date, "dd/MM/yyyy");
            return new Payment(dto.CardId, type, parsedDate, dto.Detail, dto.Comment, dto.CreditFees, dto.CreditFees)
            {
                PaymentSplits = dto.PaymentSplits.Select(splitDto => new PaymentSplit(splitDto.PersonId, splitDto.Amount)).ToList(),
            };
        }
    }
}