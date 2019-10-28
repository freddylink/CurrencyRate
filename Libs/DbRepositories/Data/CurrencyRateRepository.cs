using DbRepositories.Data.Object;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbRepositories.Data
{
    public class CurrencyRateRepository : Repository<CurrencyRate>
    {
        public CurrencyRateRepository(CurrencyRateDbContext context)
            : base(context)
        { }

        public List<CurrencyRate> GetCurrencyItems(DateTime date, string currency, string currencyDefault)
        {
            return Entities
                .Where(item => item.Timestamp.Date == date.Date
                    && (item.CurrencyCode == currency
                    || item.CurrencyCode == currencyDefault))
                .ToList();
        }
    }
}
