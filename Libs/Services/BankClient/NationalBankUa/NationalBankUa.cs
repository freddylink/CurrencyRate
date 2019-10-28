using Core.Services.Enum;
using DbRepositories.Data.Object;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Converters;
using Services.HttpModuleClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.BankClient.NationalBankUa
{
    public class NationalBankUa : IBankClient
    {
        private readonly string _url;
        private readonly string _dateFormat;

        public NationalBankUa(IConfiguration configuration) {
            _url = configuration["BankClientConfig:NationalBankUa:url"];
            _dateFormat = configuration["BankClientConfig:NationalBankUa:dateFormat"];
        }

        public async Task<IEnumerable<CurrencyRate>> GetCurrencyRatesByDate(DateTime date)
        {
            IEnumerable<CurrencyRate> currencyRates = new List<CurrencyRate>();
            List<NationalBankUaDto> responseBankClientSerialize = new List<NationalBankUaDto>();

            string responseBankClient = await HttpModule.DownloadData(UrlConfigurator(date));

            try
            {
                responseBankClientSerialize = ApiParserData(responseBankClient);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't parse response. BankClient - NationalBankUa. Error: " + ex.Message);
            }

            if (responseBankClientSerialize.Count == 0 || responseBankClientSerialize.Count == 1)
            {
                throw new Exception("NationalBankUa - Wrong date. Error date: " + date);
            }
            
            currencyRates = responseBankClientSerialize.Select(currencyItem => new CurrencyRate
            {
                BankId = BankClientType.NationalBankUa.ToString(),
                CurrencyCode = currencyItem.Code,
                Rate = currencyItem.Rate,
                Timestamp = date.Date
            });
            
            return currencyRates;
        }

        private string UrlConfigurator(DateTime date)
        {
            return _url + date.ToString(_dateFormat) + "&json";
        }

        private List<NationalBankUaDto> ApiParserData(string responseBankClient)
        {
            return JsonConvert.DeserializeObject<List<NationalBankUaDto>>(responseBankClient);
        }

    }
}
