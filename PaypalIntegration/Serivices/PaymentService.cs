using Microsoft.Extensions.Options;
using PayPal.Api;
using PaypalIntegration.Models;
using PaypalIntegration.Serivices.Interfaces;

namespace PaypalIntegration.Serivices;

internal sealed class PaymentService(IOptionsSnapshot<PaypalConfiguration> options) : IPaymentService
{
    private PaypalConfiguration PaypalConfiguration => options.Value;

    public async Task<Payment?> CreatePaymentAsync()
    {
        try
        {
            //var config = ConfigManager.Instance.GetProperties();

            Dictionary<string, string> keys = new()
            {
                {"mode",PaypalConfiguration.Mode},
                {"clientId",PaypalConfiguration.ClientId},
                {"clientSecret",PaypalConfiguration.ClientSecret}
            };
            var accessToken = new OAuthTokenCredential(PaypalConfiguration.ClientId, PaypalConfiguration.ClientSecret, keys).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            // Make an API call
            var payment = Payment.Create(apiContext, new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Transaction description.",
                        invoice_number = "001",
                        amount = new Amount
                        {
                            currency = "USD",
                            total = "10.00",
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = "https://localhost:7119/Payment/confirm",
                    cancel_url = "https://localhost:7119"
                }
            });

            return payment;
        }
        catch (Exception ex)
        {
            return default;
        }
    }
}
