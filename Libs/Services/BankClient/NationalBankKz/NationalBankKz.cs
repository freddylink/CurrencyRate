using Core.Services.Enum;
using DbRepositories.Data.Object;
using Microsoft.Extensions.Configuration;
using Services.Converters;
using Services.HttpModuleClient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BankClient.NationalBankKz
{
    public class NationalBankKz : IBankClient
    {
        private readonly string _url;
        private readonly string _dateFormat;

        public NationalBankKz(IConfiguration configuration)
        {
            _url = configuration["BankClientConfig:NationalBankKz:url"];
            _dateFormat = configuration["BankClientConfig:NationalBankKz:dateFormat"];
        }

        public async Task<IEnumerable<CurrencyRate>> GetCurrencyRatesByDate(DateTime date)
        {
            IEnumerable<CurrencyRate> currencyRates = new List<CurrencyRate>();
            NationalBankKzDto responseBankClientSerialize = new NationalBankKzDto();

            string responseBankClient = await HttpModule.DownloadData(UrlConfigurator(date));
            responseBankClientSerialize = XmlApiSerializer.DataConverterDto<NationalBankKzDto>(responseBankClient);

            if (responseBankClientSerialize.Error == "введена неверная дата")
            {
                throw new Exception("NationalBankKz - Wrong format date. Error date: " + date);
            }

            if (responseBankClientSerialize.Rates.Count == 0)
            {
                throw new Exception("NationalBankKz - returned error. Date: " + date + ". Add information: " + responseBankClientSerialize.Info);
            }

            currencyRates = responseBankClientSerialize.Rates.Select(currencyItem => new CurrencyRate
            {
                BankId = BankClientType.NationalBankKz.ToString(),
                CurrencyCode = currencyItem.Code,
                Rate = currencyItem.Rate / currencyItem.Quantity,
                Timestamp = date.Date
            });

            return currencyRates;
        }

        private string UrlConfigurator(DateTime date)
        {
            return _url + date.ToString(_dateFormat);
        }
    }
}
