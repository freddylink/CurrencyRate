using System;

namespace Services.BankClient.NationalBankKz
{
    public class NationalBankKzDto
    {
        public int NationalBankKzDtoId { get; set; }
        public string fullname { get; set; }
        public string title { get; set; }
        public float description { get; set; }
        public int quant { get; set; }
        public DateTime ExchangeDate { get; set; }
    }
}
