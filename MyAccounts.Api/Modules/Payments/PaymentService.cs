using Microsoft.EntityFrameworkCore;
using MyAccounts.Api.AppConfig.Exceptions;
using MyAccounts.Api.Database;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Modules.Payments.Dtos;
using MyAccounts.Api.Modules.Shared;

namespace MyAccounts.Api.Modules.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly MyAccountsContext _context;
        private readonly IDtoService _dtoService;

        public PaymentService(MyAccountsContext context, IDtoService dtoService)
        {
            _context = context;
            _dtoService = dtoService;
        }

        public async Task<IList<PaymentDto>> GetPaymentsByUser(int userId)
        {
            var q = from py in _context.Payments
                    join ps in _context.PaymentSplits on py.Id equals ps.PaymentId
                    join pe in _context.Persons on ps.PersonId equals pe.Id
                    where pe.UserId == userId
                    select py;
            
            var list = await q.Include(p => p.PaymentSplits)
                              .ToListAsync();

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

            var payment = await GetPaymentById(dto.Id) ?? 
                            throw new ApiClientException(Errors.NOT_FOUND("el pago", dto.Id));

            _dtoService.Map(dto, payment);

            await _context.SaveChangesAsync();

            return _dtoService.Map<PaymentDto>(payment);
        }

        private async Task<Payment?> GetPaymentById(int paymentId)
        {
            return await _context.Payments.Include(p => p.PaymentSplits)
                                    .FirstOrDefaultAsync(p => p.Id == paymentId);
        }
    }
}