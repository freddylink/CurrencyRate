using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Services.Converters
{
    public class XmlApiSerializer
    {
        public static T DataConverterDto<T>(string apiData)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("rates"));
                using (StringReader stringReader = new StringReader(apiData))
                {
                    return (T)serializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can't parse response. BankClient " + typeof(T) + " Error: " + ex.Message);
            }
        }
    }
}
