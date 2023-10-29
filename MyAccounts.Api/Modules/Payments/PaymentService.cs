using Microsoft.EntityFrameworkCore;
using MyAccounts.Api.AppConfig.Exceptions;
using MyAccounts.Api.Database.Context;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Dtos;
using MyAccounts.Api.Modules.Security;
using MyAccounts.Api.Modules.Shared;

namespace MyAccounts.Api.Modules.Payments
{
    public interface IPaymentService
    {
        public Task<IList<PaymentDto>> GetList();
        public Task<PaymentDto> CreatePayment(SavePaymentDto dto);
        public Task<PaymentDto> EditPayment(SavePaymentDto dto);
    }

    public class PaymentService : IPaymentService
    {
        private readonly MyAccountsContext _context;
        private readonly IDtoService _dtoService;
        private readonly IPrincipalService _principal;

        public PaymentService(MyAccountsContext context, IDtoService dtoService, IPrincipalService principal)
        {
            _context = context;
            _dtoService = dtoService;
            _principal = principal;
        }

        public async Task<IList<PaymentDto>> GetList()
        {
            var q = _context.Payments
                            .Include(p => p.PaymentSplits)
                            .Where(p => p.PaymentSplits.Any(s => s.PersonId == _principal.UserId))
                            .AsSplitQuery();

            var list = await q.ToListAsync();

            return _dtoService.Map<List<PaymentDto>>(list);
        }

        public async Task<PaymentDto> CreatePayment(SavePaymentDto dto)
        {
            await _dtoService.ValidateAsync(dto);

            var newPayment = _dtoService.Map<Payment>(dto);

            _context.Payments.Add(newPayment);

            await _context.SaveChangesAsync();

            return _dtoService.Map<PaymentDto>(newPayment);
        }

        public async Task<PaymentDto> EditPayment(SavePaymentDto dto)
        {
            await _dtoService.ValidateAsync(dto);

            var payment = _dtoService.Map<Payment>(dto);

            var modifiedPayment = await UpdatePayment(payment);

            await _context.SaveChangesAsync();

            return _dtoService.Map<PaymentDto>(modifiedPayment);
        }

        private async Task<Payment> UpdatePayment(Payment payment)
        {
            var originalPayment = await _context.Payments
                                                .Include(p => p.PaymentSplits)
                                                .AsSplitQuery()
                                                .FirstOrDefaultAsync(p => p.Id == payment.Id);

            if (originalPayment == null)
                throw new ApiErrorException(Errors.NOT_FOUND("pago", payment.Id));

            _context.Entry(originalPayment).CurrentValues.SetValues(payment);
            originalPayment.PaymentSplits = payment.PaymentSplits;

            return originalPayment;
        }
    }
}