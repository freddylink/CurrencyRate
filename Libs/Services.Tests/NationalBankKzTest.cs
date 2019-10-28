using Microsoft.Extensions.Configuration;
using Services.BankClient;
using Services.BankClient.NationalBankKz;
using Services.Converters;
using Services.HttpModuleClient;
using System;
using System.IO;
using System.Xml;
using Xunit;

namespace Services.Tests
{
    public class NationalBankKzTest : IClassFixture<IConfigurationFixture>
    {
        private readonly HttpModule _httpModule;
        private IConfiguration _configuration;
        private readonly IBankClient _bankClient;

        public NationalBankKzTest(IConfigurationFixture fixture)
        {
            _configuration = fixture.Configuration;
            _httpModule = new HttpModule();
            _bankClient = new NationalBankKz(_configuration);
        }

        [Fact]
        public async void NationalBankKz_GetCurrencyRatesByDate_GetRatesByCurrentDate_NoExceptions()
        {
            Exception exception = null;
            DateTime date = new DateTime(2019, 02, 01);
            try
            {
                await _bankClient.GetCurrencyRatesByDate(date);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.Null(exception);
        }

        [Fact]
        public async void NationalBankKz_GetCurrencyRatesByDate_GetRatesByPastDate_Exceptions()
        {
            DateTime date = new DateTime(1990, 10, 01);
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _bankClient.GetCurrencyRatesByDate(date));
            var a = "NationalBankKz - returned error. Date: " + date + ". Add information: на выбранную дату информации нет.";
            Assert.Equal("NationalBankKz - returned error. Date: " + date + ". Add information: на выбранную дату информации нет.", exception.Message);
        }

        [Fact]
        public async void NationalBankKz_GetCurrencyRatesByDate_GetRatesByFutureDate_Exceptions()
        {
            DateTime date = new DateTime(2990, 10, 01);
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _bankClient.GetCurrencyRatesByDate(date));
            Assert.Equal("NationalBankKz - returned error. Date: " + date + ". Add information: на выбранную дату информации нет.", exception.Message);
        }        
    }
}
