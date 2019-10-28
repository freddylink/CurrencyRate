using Core.Services.Enum;
using Core.Services.Factory;
using DbRepositories.Data.Object;
using Services.BankClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.BankClientDataProvider
{
    public class BankClientDataProvider
    {
        private readonly BankClientFactory _bankClientFactory;
        private readonly List<BankClientType> _bankClientTypes;

        public BankClientDataProvider(BankClientFactory bankClientFactory)
        {
            _bankClientFactory = bankClientFactory;
            _bankClientTypes = new List<BankClientType>
            {
                BankClientType.NationalBankKz,
                BankClientType.NationalBankUa,
                BankClientType.OpenExchangeRateBankClient
            };
        }

        public async Task<IEnumerable<CurrencyRate>> GetRatesFromBanks(DateTime date)
        {
            List<CurrencyRate> currencyRates = new List<CurrencyRate>();
            IEnumerable<Task> tasks = _bankClientTypes.Select(bankClientType => Task.Run(async () =>
                {
                    IBankClient bankClient = _bankClientFactory.GetByType(bankClientType);
                    IEnumerable<CurrencyRate> rates = await bankClient.GetCurrencyRatesByDate(date);
                    currencyRates.AddRange(rates);
                })
            );

            await Task.WhenAll(tasks);

            return currencyRates;
        }
    }
}
