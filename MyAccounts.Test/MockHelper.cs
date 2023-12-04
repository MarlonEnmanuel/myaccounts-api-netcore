using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Moq.Language.Flow;
using MyAccounts.Api.Database;
using MyAccounts.Api.Modules.Shared;

namespace MyAccounts.Test
{
    public static class MockHelper
    {
        public static readonly IMapper MapperInstance;

        static MockHelper()
        {
            var configurarion = new MapperConfiguration(cfg => {
                cfg.AddMaps("MyAccounts.Api");
            });
            MapperInstance = configurarion.CreateMapper();
        }

        public static Mock<DtoService> GetDtoServiceMock()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dtoServiceMock = new Mock<DtoService>(MapperInstance, serviceProviderMock.Object);

            // Mock de Validate (para no validar)
            dtoServiceMock.Setup(s => s.Validate(It.IsAny<object>()))
                          .Callback(() => { });

            // Mock de ValidateAsync (para no validar)
            dtoServiceMock.Setup(s => s.ValidateAsync(It.IsAny<object>()))
                          .Returns(() => Task.CompletedTask);

            return dtoServiceMock;
        }

        public static Mock<MyAccountsContext> GetMyAccountsContextMock(FakeMyAccountsDb fakeDb)
        {
            var contextMock = new Mock<MyAccountsContext>();

            // Mock de SaveChanges y SaveChangesAsync
            contextMock.Setup(c => c.SaveChanges()).Returns(1);
            contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .Returns((CancellationToken token) => Task.FromResult(1));

            // Mock de los DBSets
            contextMock.Setup(c => c.Users).MockDbSet(fakeDb.Users, u => u.Id);
            contextMock.Setup(c => c.Persons).MockDbSet(fakeDb.Persons, p => p.Id);
            contextMock.Setup(c => c.Cards).MockDbSet(fakeDb.Cards, c => c.Id);
            contextMock.Setup(c => c.Payments).MockDbSet(fakeDb.Payments, p => p.Id);
            contextMock.Setup(c => c.PaymentSplits).MockDbSet(fakeDb.PaymentSplits, p => $"{p.PersonId}{p.PaymentId}");

            return contextMock;
        }

        public static IReturnsResult<TMock> MockDbSet<TMock, TEntity>(
            this ISetup<TMock, DbSet<TEntity>> setup,
            List<TEntity> sourceList,
            Func<TEntity, object> identifierSelector
        )
            where TMock : class
            where TEntity : class
        {
            TEntity? find(object[] ids)
            {
                if (ids == null || ids.Length == 0) return null;
                object identifier = ids.Length == 1 ? ids[0] : string.Join(string.Empty, ids);
                return sourceList.FirstOrDefault(e => identifierSelector(e).Equals(identifier));
            }

            var mock = new Mock<DbSet<TEntity>>();

            // Mock para Add
            mock.Setup(d => d.Add(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity => sourceList.Add(entity));

            // Mock para AddRange
            mock.Setup(d => d.AddRange(It.IsAny<IEnumerable<TEntity>>()))
                .Callback<IEnumerable<TEntity>>(entities => sourceList.AddRange(entities));

            // Mock para Remove
            mock.Setup(d => d.Remove(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity => sourceList.Remove(entity));

            // Mock para RemoveRange
            mock.Setup(d => d.RemoveRange(It.IsAny<IEnumerable<TEntity>>()))
                .Callback<IEnumerable<TEntity>>(entities => { foreach (var e in entities) sourceList.Remove(e); });

            // Mock para Find
            mock.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => find(ids));

            // Mock para FindAsync
            mock.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(ids => ValueTask.FromResult(find(ids)));

            // Mock para consultas
            return setup.ReturnsDbSet(sourceList, mock);
        }
    }
}
