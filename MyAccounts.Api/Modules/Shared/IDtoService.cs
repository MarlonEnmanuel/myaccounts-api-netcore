namespace MyAccounts.Api.Modules.Shared
{
    public interface IDtoService
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination target);
        void Validate<TDto>(TDto instance);
        Task ValidateAsync<TDto>(TDto instance);
    }
}
