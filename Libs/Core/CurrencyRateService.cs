using DbRepositories.CurrencyRates.Object;
using DbRepositories.CurrencyRates.Repository;
using Services.BankClient.NationalBankKz;
using Services.BankClient.NationalBankUa;
using Services.BankClient.OpenExchangeRates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core
{
    public class CurrencyRateService
    {
        private readonly NationalBankUa _nationalBankUa;
        private readonly NationalBankKz _nationalBankKz;
        private readonly OpenExchangeRates _openExchangeRates;
        private readonly CurrencyRateRepository _currencyRateRepository;

        public CurrencyRateService(
            NationalBankUa nationalBankUa, 
            NationalBankKz nationalBankKz, 
            OpenExchangeRates openExchangeRates, 
            CurrencyRateRepository currencyRateRepository)
        {
            _nationalBankUa = nationalBankUa;
            _nationalBankKz = nationalBankKz;
            _openExchangeRates = openExchangeRates;
            _currencyRateRepository = currencyRateRepository;
        }

        public async Task<string> GetCurrencyRate(DateTime date)
        {
            List<NationalBankUaDto> result = await _nationalBankUa.GetCurrencyByDate(date);
            List<NationalBankKzDto> result2 = await _nationalBankKz.GetCurrencyByDate(date);
            List<OpenExchangeRatesDto> result3 = await _openExchangeRates.GetCurrencyByDate(date);

            foreach (NationalBankUaDto currencyDto in result)
            {
                CurrencyRate currencyRate = new CurrencyRate
                {
                    BankId = 1,
                    CurrencyCode = currencyDto.Txt.ToString(),
                    Timestamp = currencyDto.ExchangeDate,
                    Rate = currencyDto.Rate
                };
                _currencyRateRepository.Add(currencyRate);
            }

            foreach (NationalBankKzDto currencyDto in result2)
            {
                CurrencyRate currencyRate = new CurrencyRate
                {
                    BankId = 2,
                    CurrencyCode = currencyDto.fullname.ToString(),
                    Timestamp = currencyDto.ExchangeDate,
                    Rate = currencyDto.description
                };
                _currencyRateRepository.Add(currencyRate);
            }

            foreach (OpenExchangeRatesDto currencyDto in result3)
            {
                CurrencyRate currencyRate = new CurrencyRate
                {
                    BankId = 3,
                    CurrencyCode = currencyDto.Code.ToString(),
                    Timestamp = currencyDto.ExchangeDate,
                    Rate = currencyDto.Rate
                };
                _currencyRateRepository.Add(currencyRate);
            }


            File.WriteAllText("D:\\new_file.txt", result3.ToString());
            return result3.ToString();
        }
    }
}
