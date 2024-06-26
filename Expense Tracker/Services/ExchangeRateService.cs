using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Expense_Tracker.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<float> GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            var response = await _httpClient.GetStringAsync($"https://api.exchangeratesapi.io/latest?base={baseCurrency}&symbols={targetCurrency}");
            var data = JObject.Parse(response);
            return data["rates"][targetCurrency].Value<float>();
        }
    }
}

