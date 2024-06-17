using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaypalIntegration.Serivices.Interfaces;

namespace PaypalIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService service) : ControllerBase
    {


        [HttpPost("pay")]
        public async Task<IActionResult> CreatePaymentAsync()
        {
            var payment = await service.CreatePaymentAsync();
            if (payment is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Payment creation failed.");

            return Ok(payment);
        }

        [HttpGet("confirm")]
        public IActionResult ConfirmPayment(string paymentID, string token, string payerID)
        {
            return Ok("Payment confirmed.");
        }
    }
}
