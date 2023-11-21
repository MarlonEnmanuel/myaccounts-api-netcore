using AutoMapper;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Modules.Payments.Dtos;

namespace MyAccounts.Api.Modules.Payments
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentSplit, PaymentSplitDto>();
            CreateMap<SavePaymentDto, Payment>();
            CreateMap<SavePaymentSplitDto, PaymentSplit>();
        }
    }
}
