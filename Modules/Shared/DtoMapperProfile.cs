using AutoMapper;
using MyAccounts.Database.Models;
using MyAccounts.Dtos;

namespace MyAccounts.Modules.Shared
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Person, PersonDto>();
            CreateMap<Card, CardDto>();

            CreateMap<InputPaymentDto, Payment>();
            CreateMap<InputPaymentSplitDto, PaymentSplit>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentSplit, PaymentSplitDto>();
        }
    }
}
