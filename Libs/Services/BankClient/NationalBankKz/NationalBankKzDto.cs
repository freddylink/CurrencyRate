using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Services.BankClient.NationalBankKz
{
    [Serializable]
    public class NationalBankKzDto
    {
        [XmlElement("generator")]
        public string Generator { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("item")]
        public List<NationalBankKzItemDto> Rates;

        [XmlElement("info")]
        public string Info { get; set; }

        [XmlElement("error")]
        public string Error { get; set; }
    }
}
