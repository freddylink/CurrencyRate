using System;

namespace Services.BankClient.NationalBankUa
{
    public class NationalBankUaDto
    {
        public int NationalBankUaDtoId { get; set; }
        public int R030 { get; set; }
        public string Txt { get; set; }
        public float Rate { get; set; }
        public string Cc { get; set; }
        public DateTime ExchangeDate { get; set; }
    }
}
