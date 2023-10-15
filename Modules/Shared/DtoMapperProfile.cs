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

            CreateMap<SavePaymentDto, Payment>();
            CreateMap<SavePaymentSplitDto, PaymentSplit>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentSplit, PaymentSplitDto>();
        }
    }
}
