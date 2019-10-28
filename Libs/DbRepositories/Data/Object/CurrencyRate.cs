using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbRepositories.Data.Object
{
    public class CurrencyRate
    {
        public int CurrencyRateId { get; set; }
        public string BankId { get; set; }
        public string CurrencyCode { get; set; }
        public float Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
