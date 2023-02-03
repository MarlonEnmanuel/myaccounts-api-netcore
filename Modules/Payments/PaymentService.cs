using AutoMapper;
using MyAccounts.Database.Context;
using MyAccounts.Database.Enums;
using MyAccounts.Database.Models;
using MyAccounts.Modules.Payments.Dto;

namespace MyAccounts.Modules.Payments
{
    public interface IPaymentService
    {
        public Task<PaymentDto> CreatePayment(InputPaymentDto dto);
    }

    public class PaymentService : IPaymentService
    {
        private readonly MyAccountsContext _context;
        private readonly IMapper _mapper;

        public PaymentService(MyAccountsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaymentDto> CreatePayment(InputPaymentDto dto)
        {
            var card = await _context.Cards.FindAsync(dto.CardId) ?? throw new Exception("Tarjeta no existe");

            var newPayment = _mapper.Map<Payment>(dto);
            newPayment.CardId = card.Id;

            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(newPayment);
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