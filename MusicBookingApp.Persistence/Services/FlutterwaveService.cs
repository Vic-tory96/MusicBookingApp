using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Application.Settings;

namespace MusicBookingApp.Persistence.Services
{
    public class FlutterwaveService : IFlutterwaveService
    {
        private readonly HttpClient _httpClient;
        private readonly FlutterwaveSettings _settings;

        public FlutterwaveService(HttpClient httpClient, IOptions<FlutterwaveSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }
        public async Task<string> InitializePaymentAsync(decimal amount, string email, string txRef)
        {
            var payload = new
            {
                tx_ref = txRef,
                amount = amount,
                currency = "NGN",
                redirect_url = "https://yourwebsite.com/payment/callback",  // Placeholder
                payment_options = "card,banktransfer,ussd",
                customer = new { email = email },
                customizations = new { title = "Music Booking Payment", description = "Payment for event booking" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.flutterwave.com/v3/payments")
            {
                Headers = { { "Authorization", $"Bearer {_settings.SecretKey}" } },
                Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);
            return jsonResponse.GetProperty("data").GetProperty("link").GetString();  // Return the payment link
        }

        public async Task<bool> VerifyPaymentAsync(string transactionId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.flutterwave.com/v3/transactions/{transactionId}/verify")
            {
                Headers = { { "Authorization", $"Bearer {_settings.SecretKey}" } }
            };

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);
            return jsonResponse.GetProperty("status").GetString() == "success";
        }
    }
}
