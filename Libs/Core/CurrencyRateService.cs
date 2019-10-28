using DbRepositories.Data;
using DbRepositories.Data.Object;
using Services.BankClientDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly CurrencyRateRepository _currencyRateRepository;
        private readonly BankClientDataProvider _bankClientDataProvider;

        public CurrencyRateService(CurrencyRateRepository currencyRateRepository, BankClientDataProvider bankClientDataProvider)
        {
            _currencyRateRepository = currencyRateRepository;
            _bankClientDataProvider = bankClientDataProvider;
        }

        public async Task<List<CurrencyRate>> GetCurrencyRates(DateTime date, string currency, string currencyDefault)
        {
            List<CurrencyRate> currencyRates = _currencyRateRepository.GetCurrencyItems(date, currency, currencyDefault);

            if (currencyRates.Select(c => c.BankId).ToList().Distinct().Count() == 0)
            {
                IEnumerable<CurrencyRate> rates = await _bankClientDataProvider.GetRatesFromBanks(date);
                _currencyRateRepository.Add(rates);
                currencyRates.AddRange(rates);
            }

            return currencyRates;
        }
    }
}