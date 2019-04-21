using System;

namespace DbRepositories.CurrencyRates.Object
{
    public class CurrencyRate
    {
        public int CurrencyRateId { get; set; }
        public int BankId { get; set; }
        public string CurrencyCode { get; set; }
        public float Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
