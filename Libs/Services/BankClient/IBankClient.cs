using DbRepositories.Data.Object;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BankClient
{
    public interface IBankClient
    {
        Task<IEnumerable<CurrencyRate>> GetCurrencyRatesByDate(DateTime date);
    }
}
