using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.HttpModuleClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BankClient.NationalBankUa
{
    public class NationalBankUa : BankClient 
    {
        private readonly HttpModule _httpModule;
        private readonly string _url;

        public NationalBankUa(HttpModule httpModule, IConfiguration configuration) {
            _httpModule = httpModule;
            _url = configuration["BankClientConfig:NationalBankUa:url"];
        }

        private string UrlConfigurator(DateTime date)
        {
            return _url + date.ToString("yyyyMMdd") + "&json";
        }

        public async Task<List<NationalBankUaDto>> GetCurrencyByDate(DateTime date)
        {
            List<NationalBankUaDto> currencyRate = new List<NationalBankUaDto>();
            dynamic result = ApiParserData(await _httpModule.GetDataClient(UrlConfigurator(date)));
            
            foreach (dynamic item in result)
            {
                NationalBankUaDto data = new NationalBankUaDto
                {
                    R030 = item["r030"],
                    Txt = item["txt"],
                    Rate = item["rate"],
                    Cc = item["cc"]
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
