using Microsoft.AspNetCore.Mvc;
using MyAccounts.Database.Models;
using MyAccounts.Modules.Payments;
using MyAccounts.Modules.Payments.Dto;

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
            var payment = await _paymentService.CreatePayment(dto);
            return payment;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
