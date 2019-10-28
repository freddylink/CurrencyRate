using Core;
using DbRepositories.Data;
using DbRepositories.Data.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        public string DateSelected;
        public string CurrencySelected;
        public string ErrorRequestUser;
        public List<CurrencyRate> CurrencyRatesConverted = new List<CurrencyRate>();
        private readonly ICurrencyRateService _currencyRateService;
        private readonly LogRepository _logRepository;
        private readonly string _currencyDefault;
        private readonly IConfiguration _configuration;
        public List<string> CurrenciesSelected { get; } = new List<string>();

        public IndexModel(ICurrencyRateService currencyRateService, IConfiguration configuration, LogRepository logRepository)
        {
            _currencyRateService = currencyRateService;
            _configuration = configuration;
            _logRepository = logRepository;
            CurrenciesSelected = configuration.GetSection("CurrenciesSelected:Currencies").Get<List<string>>();
            _currencyDefault = configuration.GetSection("CurrenciesSelected:DefaultCurrency").Get<string>();
        }

        public void OnGet()
        {
            DateSelected = DateTime.Today.ToString("yyyy-MM-dd");
        }

        public async Task<ActionResult> OnPostAsync(DateTime datePicker, string currency)
        {
            DateSelected = datePicker.ToString("yyyy-MM-dd");
            CurrencySelected = currency;
            try
            {
                List<CurrencyRate> currencyRatesData = await _currencyRateService.GetCurrencyRates(datePicker.Date, currency, _currencyDefault);
                CurrencyRatesConverted = ConvertRatesToFrontEnd(currencyRatesData, currency, _currencyDefault);
            }
            catch (Exception ex)
            {
                _logRepository.AddException(ex.Message);
                ErrorRequestUser = "Ваш запрос на дату: " + DateSelected + " не был обработан. Произошла ошибка!";
            }

            return Page();
        }

        private List<CurrencyRate> ConvertRatesToFrontEnd(List<CurrencyRate> currencyRates, string selectedCurrency, string currencyDefault)
        {
            List<CurrencyRate> filteredCurency = currencyRates.Where(c => c.CurrencyCode == selectedCurrency).ToList();
            List<CurrencyRate> filteredCurrencyDafault = currencyRates.Where(c => c.CurrencyCode == currencyDefault).ToList();

            foreach (CurrencyRate rateBySelectedCurrency in filteredCurency)
            {
                CurrencyRate rateByDefaultCurrency = filteredCurrencyDafault.Find(item => item.BankId == rateBySelectedCurrency.BankId);
                if (rateBySelectedCurrency.Rate != 1)
                {
                    rateBySelectedCurrency.Rate = rateBySelectedCurrency.Rate > rateByDefaultCurrency.Rate
                        ? rateBySelectedCurrency.Rate / rateByDefaultCurrency.Rate
                        : rateByDefaultCurrency.Rate / rateBySelectedCurrency.Rate;
                }
                else
                {
                    rateBySelectedCurrency.Rate = rateByDefaultCurrency.Rate;
                }
            }

            return filteredCurency;
        }
    }
}