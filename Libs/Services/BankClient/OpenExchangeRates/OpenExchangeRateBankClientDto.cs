using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.BankClient.OpenExchangeRates
{
    [DataContract]
    public class OpenExchangeRateBankClientDto
    {
        [DataMember(Name = "disclaimer")]
        public string Disclaimer { get; set; }

        [DataMember(Name = "licence")]
        public string License { get; set; }

        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }

        [DataMember(Name = "base")]
        public string Base { get; set; }

        [DataMember(Name = "error")]
        public bool Error { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "rates")]
        public Dictionary<string, float> Rates;
    }
}
