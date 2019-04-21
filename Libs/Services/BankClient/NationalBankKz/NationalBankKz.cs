using Microsoft.Extensions.Configuration;
using Services.HttpModuleClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Services.BankClient.NationalBankKz
{
    public class NationalBankKz : BankClient
    {
        private readonly HttpModule _httpModule;
        private readonly string _url;

        public NationalBankKz(HttpModule httpModule, IConfiguration configuration)
        {
            _httpModule = httpModule;
            _url = configuration["BankClientConfig:NationalBankKz:url"];
        }

        private string UrlConfigurator(DateTime date)
        {
            return _url + date.ToShortDateString();
        }

        public async Task<List<NationalBankKzDto>> GetCurrencyByDate(DateTime date)
        {
            List<NationalBankKzDto> currencyRate = new List<NationalBankKzDto>();
            string xmlData = await _httpModule.GetDataClient(UrlConfigurator(date));
            XmlNodeList xmlList = ApiParserData(xmlData);
            foreach (XmlNode item in xmlList)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(NationalBankKzDto), new XmlRootAttribute("item"));
                StringReader stringReader = new StringReader(item.OuterXml);
                NationalBankKzDto data = (NationalBankKzDto)serializer.Deserialize(stringReader);
                currencyRate.Add(data);
            }

            return currencyRate;
        }

        private XmlNodeList ApiParserData(string apiData)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(apiData);
            XmlNodeList xmlList = xmlDocument.SelectNodes("/rates/item");

            return xmlList;
        }
    }
}
