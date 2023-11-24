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
        public void GetAuthData_ShouldThrowErrorIsUserNotExists()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _service.GetAuthData(99);
            });
        }
    }
}
