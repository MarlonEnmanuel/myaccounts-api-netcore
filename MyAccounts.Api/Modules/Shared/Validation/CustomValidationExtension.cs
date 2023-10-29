using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyAccounts.Api.Database.Interfaces;

namespace MyAccounts.Api.Modules.Shared.Validation
{
    public static class CustomValidationExtension
    {
        private const string ENTITY_NOT_FOUND = "El registro no existe";
        private const string ENTITYS_NOT_FOUND = "Uno o más registros no existen";
        private const string REPEATED_DATA = "Algunos datos son repetidos";

        public static IRuleBuilderOptions<T, int> AppExistsIdAsync<T, TEntity>
        (
            this IRuleBuilder<T, int> ruleBuilder,
            DbSet<TEntity> dbSet
        )
            where TEntity : class
        {
            var validate = async (int id, CancellationToken token) =>
            {
                var entity = await dbSet.FindAsync(id, token);
                return entity != null;
            };
            return ruleBuilder.MustAsync(validate).WithMessage(ENTITY_NOT_FOUND);
        }

        public static IRuleBuilderOptions<T, IList<TDto>> AppExistsIdsAsync<T, TDto, TEntity>
        (
            this IRuleBuilder<T, IList<TDto>> ruleBuilder,
            DbSet<TEntity> dbSet,
            Func<TDto, int> getDtoId
        )
            where TEntity : class, IIdentity
        {
            var validate = async (IList<TDto> dtoList, CancellationToken token) =>
            {
                var ids = dtoList.Select(getDtoId).Distinct();
                var entityList = await dbSet.Where(e => ids.Contains(e.Id))
                                            .ToListAsync(token);
                return ids.Count() == entityList.Count;
            };
            return ruleBuilder.MustAsync(validate).WithMessage(ENTITYS_NOT_FOUND);
        }

        public static IRuleBuilderOptions<T, IList<TDto>> AppDisctinctValues<T, TDto>
        (
            this IRuleBuilder<T, IList<TDto>> ruleBuilder,
            Func<TDto, int> getValue
        )
        {
            var validate = (IList<TDto> dtoList) =>
            {
                return !dtoList.Select(getValue)
                                .GroupBy(x => x)
                                .Where(g => g.Count() > 1)
                                .Any();
            };
            return ruleBuilder.Must(validate).WithMessage(REPEATED_DATA);
        }
    }
}
