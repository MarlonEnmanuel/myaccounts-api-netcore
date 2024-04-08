using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Modules.General;

namespace MyAccounts.Test.Modules.General
{
    public class GeneralServiceTest
    {
        private readonly FakeMyAccountsDb _db = new();
        private readonly GeneralService _service;

        public GeneralServiceTest()
        {
            var contextMock = MockHelper.GetMyAccountsContextMock(_db);
            var dtoserviceMock = MockHelper.GetDtoServiceMock();

            _service = new GeneralService(contextMock.Object, dtoserviceMock.Object);
        }

        [Fact]
        public async Task GetAuthData_ShouldThrowErrorIsUserNotExists()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _service.GetAuthData(99);
            });
        }

        [Fact]
        public async void GetAuthData_ShouldReturnEmptyLists()
        {
            _db.Users.Add(new() { Id = 1, Username = "jhondoe" });

            var dto = await _service.GetAuthData(1);

            Assert.NotNull(dto);

            Assert.NotNull(dto.User);
            Assert.NotNull(dto.Persons);
            Assert.NotNull(dto.Cards);

            Assert.Empty(dto.Persons);
            Assert.Empty(dto.Cards);

            Assert.Equal(1, dto.User.Id);
            Assert.Equal("jhondoe", dto.User.Username);
        }

        [Fact]
        public async void GetAuthData_ShouldReturnWithPersons()
        {
            var user = new User() { Id = 1, Username = "jhondoe" };
            var persons = new List<Person>()
            {
                new() { Id = 1, Name = "Person01", IsShared = true, UserId = 1, User = user },
                new() { Id = 2, Name = "Person02", IsShared = true, UserId = 1, User = user },
                new() { Id = 3, Name = "Person03", IsShared = true, UserId = 2 },
                new() { Id = 4, Name = "Person04", IsShared = false, UserId = 2 },
            };
            _db.Users.Add(user);
            _db.Persons.AddRange(persons);

            var dto = await _service.GetAuthData(1);

            Assert.Equal(3, dto.Persons.Count);
            Assert.Contains(dto.Persons, p => p.Id == 1);
            Assert.Contains(dto.Persons, p => p.Id == 2);
            Assert.Contains(dto.Persons, p => p.Id == 3);
            Assert.DoesNotContain(dto.Persons, p => p.Id == 4);
            Assert.Empty(dto.Cards);
        }

        [Fact]
        public async void GetAuthData_ShouldReturnWithCards()
        {
            _db.Users.AddRange(new List<User>()
            {
                new() { Id = 1, Username = "jhondoe" }
            });
            _db.Persons.AddRange(new List<Person>()
            {
                new() { Id = 1, Name = "Person01", IsShared = true, UserId = 1, User = _db.Users[0] },
                new() { Id = 2, Name = "Person02", IsShared = true, UserId = 2 },
            });
            _db.Cards.AddRange(new List<Card>()
            {
                new() { Id = 1, Name = "Card01", Type = PaymentType.Debit, PersonId = 1, Person = _db.Persons[0] },
                new() { Id = 2, Name = "Card02", Type = PaymentType.Debit, PersonId = 2, Person = _db.Persons[1] },
            });
            _db.Persons.ForEach(p => p.Cards = _db.Cards.Where(c => c.PersonId == p.Id).ToList());

            var dto = await _service.GetAuthData(1);

            Assert.NotEmpty(dto.Cards);
            Assert.Single(dto.Cards);
            Assert.Contains(dto.Cards, c => c.Id == 1);
            Assert.DoesNotContain(dto.Cards, c => c.Id == 2);
        }
    }
}
