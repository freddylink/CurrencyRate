using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Services.BankClient.NationalBankKz
{
    [Serializable]
    public class NationalBankKzItemDto
    {
        [XmlElement("date")]
        public DateTime Date { get; set; }

        [XmlElement("fullname")]
        public string Fullname { get; set; }

        [XmlElement("title")]
        public string Code { get; set; }

        [XmlElement("description")]
        public float Rate { get; set; }

        [XmlElement("quant")]
        public int Quantity { get; set; }
    }
}
