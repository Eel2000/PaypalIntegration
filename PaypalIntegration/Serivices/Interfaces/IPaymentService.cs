using PayPal.Api;

namespace PaypalIntegration.Serivices.Interfaces
{
    public interface IPaymentService
    {
        public Task<Payment?> CreatePaymentAsync();
    }
}
