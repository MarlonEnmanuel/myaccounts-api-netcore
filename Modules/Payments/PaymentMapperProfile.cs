using AutoMapper;
using MyAccounts.Database.Models;
using MyAccounts.Modules.Payments.Dto;

namespace MyAccounts.Modules.Payments
{
    public class PaymentMapperProfile : Profile
    {
        public PaymentMapperProfile()
        {
            CreateMap<InputPaymentDto, Payment>();
            CreateMap<InputPaymentSplitDto, PaymentSplit>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentSplit, PaymentSplitDto>();
        }
    }
}
