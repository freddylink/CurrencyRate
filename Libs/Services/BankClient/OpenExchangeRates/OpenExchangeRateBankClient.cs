using Core.Services.Enum;
using DbRepositories.Data.Object;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using Services.Converters;
using Services.HttpModuleClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BankClient.OpenExchangeRates
{
    public class OpenExchangeRateBankClient : IBankClient
    {
        private readonly string _url;
        private readonly string _dateFormat;

        public OpenExchangeRateBankClient(IConfiguration configuration)
        {
            _url = configuration["BankClientConfig:OpenExchangeRateBankClient:url"];
            _dateFormat = configuration["BankClientConfig:OpenExchangeRateBankClient:dateFormat"];
        }

        public async Task<IEnumerable<CurrencyRate>> GetCurrencyRatesByDate(DateTime date)
        {
            IEnumerable<CurrencyRate> currencyRates = new List<CurrencyRate>();
            OpenExchangeRateBankClientDto responseBankClientSerialize = new OpenExchangeRateBankClientDto();

            string responseBankClient = await HttpModule.DownloadData(UrlConfigurator(date));

            try
            {
                responseBankClientSerialize = ApiParserData(responseBankClient);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't parse response. BankClient - OpenExchangeRateBankClient. Error: " + ex.Message);
            }

            if (responseBankClientSerialize.Error == true)
            {
                throw new Exception("OpenExchangeRateBankClient - returned error: " + responseBankClientSerialize.Message + ". Date: " + date);
            }

            currencyRates = responseBankClientSerialize.Rates.Select(currencyItem => new CurrencyRate
            {
                BankId = BankClientType.OpenExchangeRateBankClient.ToString(),
                CurrencyCode = currencyItem.Key,
                Rate = currencyItem.Value,
                Timestamp = date.Date
            });

            return currencyRates;
        }

        private string UrlConfigurator(DateTime date)
        {
            return string.Format(_url, date.ToString(_dateFormat));
        }

        private OpenExchangeRateBankClientDto ApiParserData(string responseBankClient)
        {
            return JsonConvert.DeserializeObject<OpenExchangeRateBankClientDto>(responseBankClient);
        }

    }
}
