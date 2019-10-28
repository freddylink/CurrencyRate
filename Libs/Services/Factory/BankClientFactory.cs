using Core.Services.Enum;
using Microsoft.Extensions.Configuration;
using Services.BankClient;
using Services.BankClient.NationalBankKz;
using Services.BankClient.NationalBankUa;
using Services.BankClient.OpenExchangeRates;
using System;

namespace Core.Services.Factory
{
    public class BankClientFactory
    {
        private readonly IConfiguration _configuration;

        public BankClientFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public IBankClient GetByType(BankClientType type)
        {
            switch (type)
            {
                case BankClientType.NationalBankKz:
                    return new NationalBankKz(_configuration);
                case BankClientType.NationalBankUa:
                    return new NationalBankUa(_configuration);
                case BankClientType.OpenExchangeRateBankClient:
                    return new OpenExchangeRateBankClient(_configuration);
            }
            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
