using System;
using System.Collections.Generic;
using System.Text;
using DbRepositories.Core;
using DbRepositories.CurrencyRates.Object;

namespace DbRepositories.CurrencyRates.Repository
{
    public class CurrencyRateRepository : Repository<CurrencyRate>
    {
        public CurrencyRateRepository(CurrencyRateDbContext context)
            : base(context)
        { }

    }
}
