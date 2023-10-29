using AutoMapper;

namespace MyAccounts.Api.AppConfig
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<string, DateOnly>().ConvertUsing<StringToDateOnlyTypeConverter>();
            CreateMap<DateOnly, string>().ConvertUsing<DateOnlyToStringTypeConverter>();
        }
    }

    public class StringToDateOnlyTypeConverter : ITypeConverter<string, DateOnly>
    {
        public DateOnly Convert(string source, DateOnly destination, ResolutionContext context)
        {
            return DateOnly.ParseExact(source, AppConstants.DATEONLY_FORMAT);
        }
    }

    public class DateOnlyToStringTypeConverter : ITypeConverter<DateOnly, string>
    {
        public string Convert(DateOnly source, string destination, ResolutionContext context)
        {
            return source.ToString(AppConstants.DATEONLY_FORMAT);
        }
    }
}
