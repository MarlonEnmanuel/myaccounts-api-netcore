using Moq;
using MyAccounts.Api.Database;
using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Modules.Payments;
using MyAccounts.Api.Modules.Payments.Dtos;
using MyAccounts.Api.Modules.Shared;

namespace MyAccounts.Test.Modules.Payments
{
    public class PaymentServiceTest
    {
        private readonly FakeMyAccountsDb _db = new();
        private readonly Mock<MyAccountsContext> _contextMock;
        private readonly Mock<DtoService> _dtoServiceMock;
        private readonly PaymentService _service;

        public PaymentServiceTest()
        {
            _contextMock = MockHelper.GetMyAccountsContextMock(_db);
            _dtoServiceMock = MockHelper.GetDtoServiceMock();
            _service = new(_contextMock.Object, _dtoServiceMock.Object);
        }

        [Fact]
        public async void GetPaymentsByUser_ShouldReturnEmpty()
        {
            var result = await _service.GetPaymentsByUser(1);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetPaymentsByUser_ShouldReturnUserPaymentsWithSplits()
        {
            _db.Persons.AddRange(new List<Person>()
            {
                new() { Id = 1, Name = "Person1", UserId = 1 },
                new() { Id = 2, Name = "Person2", UserId = 2 },
            });
            _db.Payments.AddRange(new List<Payment>()
            {
                new() { Id = 1, Type = PaymentType.Debit, Detail = "Detai01" },
                new() { Id = 2, Type = PaymentType.Debit, Detail = "Detai01" },
                new() { Id = 3, Type = PaymentType.Debit, Detail = "Detai01" },
            });
            _db.PaymentSplits.AddRange(new List<PaymentSplit>()
            {
                new() { PersonId = 1, PaymentId = 1 , Amount = 10 },
                new() { PersonId = 1, PaymentId = 2 , Amount = 20 },
                new() { PersonId = 2, PaymentId = 2 , Amount = 30 },
                new() { PersonId = 2, PaymentId = 3 , Amount = 40 },
            });
            _db.Persons.ForEach(p => p.PaymentSplits = _db.PaymentSplits.Where(s => s.PersonId == p.Id).ToList());
            _db.Payments.ForEach(p => p.PaymentSplits = _db.PaymentSplits.Where(s => s.PersonId == p.Id).ToList());

            var result = await _service.GetPaymentsByUser(1);

            Assert.Equal(2, result.Count);
            Assert.DoesNotContain(result, p => p.Id == 3);
            Assert.DoesNotContain(result, p => p.PaymentSplits == null);
            Assert.Contains(result, p => p.PaymentSplits.Count == 2);
        }

        [Fact]
        public async void CreatePayment_ShouldSavePaymentAndSplits()
        {
            var dto = new SavePaymentDto()
            {
                Id = 1,
                Date = "2023-01-01",
                Detail = "Detail01",
                PaymentSplits = new()
                {
                    new() { PersonId = 1, Amount = 10 },
                    new() { PersonId = 2, Amount = 20 },
                },
            };

            var result = await _service.CreatePayment(dto);

            Assert.NotNull(result);
            Assert.Equal("Detail01", result.Detail);
            Assert.NotNull(result.PaymentSplits);
            Assert.NotEmpty(result.PaymentSplits);
            Assert.Equal(2, result.PaymentSplits.Count);
            Assert.Single(_db.Payments);
            _dtoServiceMock.Verify(c => c.ValidateAsync(It.IsAny<SavePaymentDto>()));
            _contextMock.Verify(c => c.SaveChangesAsync(default));
        }

        [Fact]
        public async void EditPayment_ShouldUpdatePaymentAndSplits()
        {
            _db.Payments.Add(new()
            {
                Id = 1,
                Date = new DateOnly(2023, 01, 01),
                Detail = "Detail01",
                PaymentSplits = new()
                {
                    new() { PaymentId = 1, PersonId = 1, Amount = 10 },
                },
            });

            var dto = new SavePaymentDto()
            {
                Id = 1,
                Date = "2023-01-02",
                Detail = "Detail01 update",
                PaymentSplits = new()
                {
                    new() { PersonId = 1, Amount = 15 },
                    new() { PersonId = 1, Amount = 25 },
                },
            };

            var result = await _service.EditPayment(dto);

            Assert.NotNull(result);
            Assert.Equal(new DateOnly(2023, 01, 02), result.Date);
            Assert.Equal("Detail01 update", result.Detail);
            Assert.NotNull(result.PaymentSplits);
            Assert.Single(_db.Payments);
        }
    }
}
