using AutoMapper;
using FluentValidation;

namespace MyAccounts.Modules.Shared
{
    public interface IDtoService
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination target);

        void Validate<TDto>(TDto instance);
        Task ValidateAsync<TDto>(TDto instance);
    }

    public class DtoService : IDtoService
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public DtoService(IMapper mapper, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination target)
        {
            return _mapper.Map(source, target);
        }

        public void Validate<T>(T instance)
        {
            _serviceProvider.GetRequiredService<IValidator<T>>().ValidateAndThrow(instance);
        }

        public Task ValidateAsync<T>(T instance)
        {
            return _serviceProvider.GetRequiredService<IValidator<T>>().ValidateAndThrowAsync(instance);
        }
    }
}
