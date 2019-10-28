using Microsoft.Extensions.Configuration;
using Services.BankClient;
using Services.BankClient.NationalBankKz;
using Services.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Services.Tests
{
    public class XmlApiSerializerTests : IClassFixture<IConfigurationFixture>
    {
        private IConfiguration _configuration;

        public XmlApiSerializerTests(IConfigurationFixture fixture)
        {
            _configuration = fixture.Configuration;
        }

        [Fact]
        public void XmlApiSerializer_DataCoverterDto_NationalBankKz_ValidData_Success()
        {
            string validFileAbsolutePath = Path.GetFullPath("..\\..\\..\\TestData\\NationalBankKz\\valid_bank_response.xml", Directory.GetCurrentDirectory());
            string validContent = File.ReadAllText(validFileAbsolutePath);
            NationalBankKzDto dtoData = XmlApiSerializer.DataConverterDto<NationalBankKzDto>(validContent);
            Assert.IsType<NationalBankKzDto>(dtoData);
            Assert.Equal(39, dtoData.Rates.Count);
        }

        [Fact]
        public void XmlApiSerializer_DataCoverterDto_NationalBankKz_InValidData_Exception()
        {
            string invalidFileAbsolutePath = Path.GetFullPath("..\\..\\..\\TestData\\NationalBankKz\\invalid_bank_response.xml", Directory.GetCurrentDirectory());
            var invalidContent = File.ReadAllText(invalidFileAbsolutePath);
            Assert.Throws<Exception>(() => XmlApiSerializer.DataConverterDto<NationalBankKzDto>(invalidContent));
        }
    }
}
