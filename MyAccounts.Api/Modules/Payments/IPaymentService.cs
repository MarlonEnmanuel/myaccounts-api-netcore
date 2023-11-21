using MyAccounts.Api.Modules.Payments.Dtos;

namespace MyAccounts.Api.Modules.Payments
{
    public interface IPaymentService
    {
        public Task<IList<PaymentDto>> GetPaymentsByUser(int userId);
        public Task<PaymentDto> CreatePayment(SavePaymentDto dto);
        public Task<PaymentDto> EditPayment(SavePaymentDto dto);
    }
}
