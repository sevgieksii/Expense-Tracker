using Expense_Tracker.Models;
using Expense_Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Expense_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExchangeRateService _exchangeRateService;

        public HomeController(ILogger<HomeController> logger, ExchangeRateService exchangeRateService)
        {
            _logger = logger;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<IActionResult> Index()
        {
            float exchangeRate = 0;

            try
            {
                exchangeRate = await _exchangeRateService.GetExchangeRate("USD", "TRY");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching exchange rate: {ex.Message}");
            }

            ViewData["ExchangeRate"] = exchangeRate;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

