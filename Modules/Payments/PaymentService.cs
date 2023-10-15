using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyAccounts.AppConfig.Exceptions;
using MyAccounts.Database.Context;
using MyAccounts.Database.Models;
using MyAccounts.Dtos;
using MyAccounts.Modules.Payments.Validators;
using MyAccounts.Modules.Security;
using MyAccounts.Modules.Shared;
using MyAccounts.Modules.Shared.Validation;

namespace MyAccounts.Modules.Payments
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
        private readonly IMapper _mapper;
        private readonly IValidatorService _validator;
        private readonly IPrincipalService _principal;

        public PaymentService(MyAccountsContext context, IMapper mapper, IValidatorService validator, IPrincipalService principal)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _principal = principal;
        }

        public async Task<IList<PaymentDto>> GetList()
        {
            var q = _context.Payments
                            .Include(p => p.PaymentSplits)
                            .Where(p => p.PaymentSplits.Any(s => s.PersonId == _principal.UserId))
                            .AsSplitQuery();

            var list = await q.ToListAsync();

            return _mapper.Map<List<PaymentDto>>(list);
        }

        public async Task<PaymentDto> CreatePayment(SavePaymentDto dto)
        {
            await _validator.GetDtoValidator<SavePaymentDtoValidator>().ValidateAndThrowAsync(dto);

            var newPayment = _mapper.Map<Payment>(dto);

            _context.Payments.Add(newPayment);

            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(newPayment);
        }

        public async Task<PaymentDto> EditPayment(SavePaymentDto dto)
        {
            await _validator.GetDtoValidator<SavePaymentDtoValidator>().ValidateAndThrowAsync(dto);

            var payment = _mapper.Map<Payment>(dto);

            var modifiedPayment = await UpdatePayment(payment);

            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(modifiedPayment);
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