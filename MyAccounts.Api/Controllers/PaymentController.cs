using Microsoft.AspNetCore.Mvc;
using MyAccounts.Api.AppConfig.Exceptions;
using MyAccounts.Api.Modules.Payments;
using MyAccounts.Api.Modules.Payments.Dtos;
using MyAccounts.Api.Modules.Shared;

namespace MyAccounts.Api.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDto>> Get()
        {
            return await _paymentService.GetPaymentsByUser(UserId);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<PaymentDto> Post([FromBody] SavePaymentDto dto)
        {
            return await _paymentService.CreatePayment(dto);
        }

        [HttpPut("{id}")]
        public async Task<PaymentDto> Put(int id, [FromBody] SavePaymentDto dto)
        {
            if (dto.Id != id)
                throw new ApiClientException(Errors.DTO_ID_ERROR);

            return await _paymentService.EditPayment(dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
