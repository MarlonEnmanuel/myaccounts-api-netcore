using AutoMapper;
using FluentValidation;
using MyAccounts.Database.Context;
using MyAccounts.Database.Models;
using MyAccounts.Modules.Payments.Dto;
using MyAccounts.Modules.Payments.Validators;
using MyAccounts.Modules.Shared.Validation;

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
        private readonly IValidatorService _validator;

        public PaymentService(MyAccountsContext context, IMapper mapper, IValidatorService validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PaymentDto> CreatePayment(InputPaymentDto dto)
        {
            await _validator.GetDtoValidator<InputPaymentDtoValidator>().ValidateAndThrowAsync(dto);

            var newPayment = _mapper.Map<Payment>(dto);

            _context.Payments.Add(newPayment);

            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(newPayment);
        }
    }
}