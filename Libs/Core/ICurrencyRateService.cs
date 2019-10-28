using DbRepositories.Data.Object;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface ICurrencyRateService
    {
        Task<List<CurrencyRate>> GetCurrencyRates(DateTime date, string currency, string currencyDefault);
    }
}
