using AutoMapper;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Dtos;

namespace MyAccounts.Api.Modules.Shared
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
