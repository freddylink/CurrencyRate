using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.HttpModuleClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BankClient.OpenExchangeRates
{
    public class OpenExchangeRates : BankClient
    {

        //const string url = "http://openexchangerates.org/api/latest.json?app_id=9db8f21188ad40209915838304bca2b4";
        private readonly HttpModule _httpModule;
        private readonly string _url;
        private readonly string _urlForetimeBegin;
        private readonly string _urlForetimeEnd;

        public OpenExchangeRates(HttpModule httpModule, IConfiguration configuration)
        {
            _httpModule = httpModule;
            _url = configuration["BankClientConfig:OpenExchangeRates:url"];
            _urlForetimeBegin = configuration["BankClientConfig:OpenExchangeRates:urlForetimeBegin"];
            _urlForetimeEnd = configuration["BankClientConfig:OpenExchangeRates:urlForetimeEnd"];
        }

        private string UrlConfigurator(DateTime date)
        {
            if (date == DateTime.Today)
            {
                return _url;
            } else
            {
                return _urlForetimeBegin + date.ToString("yyyy-MM-dd") + _urlForetimeEnd;
            }
        }

        public async Task<List<OpenExchangeRatesDto>> GetCurrencyByDate(DateTime date)
        {
            List<OpenExchangeRatesDto> currencyRate = new List<OpenExchangeRatesDto>();
            var a = UrlConfigurator(date);
            dynamic result = ApiParserData(await _httpModule.GetDataClient(UrlConfigurator(date)));

            foreach (dynamic item in result.rates)
            {
                OpenExchangeRatesDto data = new OpenExchangeRatesDto
                {
                    Code = item.Name,
                    Rate = item.Value,
                };
                
                currencyRate.Add(data);
            }

            return currencyRate;
        }

        private dynamic ApiParserData(string apiData)
        {
            dynamic myNewObject = JsonConvert.DeserializeObject(apiData);

            return myNewObject;
        }

    }
}
