using System;
using System.Runtime.Serialization;

namespace Services.BankClient.NationalBankUa
{
    [DataContract]
    public class NationalBankUaDto
    {
        [DataMember(Name = "R030")]
        public int R030 { get; set; }

        [DataMember(Name = "Txt")]
        public string Txt { get; set; }

        [DataMember(Name = "Rate")]
        public float Rate { get; set; }

        [DataMember(Name = "Cc")]
        public string Code { get; set; }

        [DataMember(Name = "Message")]
        public string Message { get; set; }

    }
}
