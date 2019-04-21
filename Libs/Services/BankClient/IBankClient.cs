using DbRepositories.CurrencyRates.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BankClient
{
    public interface IBankClient
    {
        List<CurrencyRate> GetCurrencyByDate(DateTime date);
    }
}
