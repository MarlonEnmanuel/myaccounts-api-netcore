using Microsoft.AspNetCore.Mvc;
using MyAccounts.AppConfig.Exceptions;
using MyAccounts.Modules.Payments;
using MyAccounts.Modules.Payments.Dto;
using MyAccounts.Modules.Shared;

namespace MyAccounts.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<PaymentDto> Post([FromBody] InputPaymentDto dto)
        {
            return await _paymentService.CreatePayment(dto);
        }

        [HttpPut("{id}")]
        public async Task<PaymentDto> Put(int id, [FromBody] InputPaymentDto dto)
        {
            if (dto.Id != id)
                throw new AppClientException(Errors.DTO_ID_ERROR);

            return await _paymentService.EditPayment(dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
