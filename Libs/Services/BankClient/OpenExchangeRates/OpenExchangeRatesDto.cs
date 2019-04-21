using System;

namespace Services.BankClient.OpenExchangeRates
{
    public class OpenExchangeRatesDto
    {
        public int OpenExchangeRatesDtoId { get; set; }
        public string Code { get; set; }
        public float Rate { get; set; }
        public DateTime ExchangeDate { get; set; }
    }
}
