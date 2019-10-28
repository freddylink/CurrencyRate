using Microsoft.Extensions.Configuration;
using Services.BankClient;
using Services.BankClient.NationalBankUa;
using Services.Converters;
using Services.HttpModuleClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Xunit;

namespace Services.Tests
{
    public class NationalBankUaTest : IClassFixture<IConfigurationFixture>
    {
        private IConfiguration _configuration;
        private readonly IBankClient _bankClient;

        public NationalBankUaTest(IConfigurationFixture fixture)
        {
            _configuration = fixture.Configuration;
            _bankClient = new NationalBankUa(_configuration);
        }

        [Fact]
        public async void NationalBankUa_GetCurrencyRatesByDate_GetRatesByCurrentDate_NoExceptions()
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
        public async void NationalBankUa_GetCurrencyRatesByDate_GetRatesByPastDate_Exceptions()
        {
            DateTime date = new DateTime(1990, 10, 01);
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _bankClient.GetCurrencyRatesByDate(date));
            Assert.Equal("NationalBankUa - Wrong date. Error date: " + date, exception.Message);
        }

        [Fact]
        public async void NationalBankUa_GetCurrencyRatesByDate_GetRatesByFutureDate_Exceptions()
        {
            DateTime date = new DateTime(2990, 10, 01);
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _bankClient.GetCurrencyRatesByDate(date));
            Assert.Equal("NationalBankUa - Wrong date. Error date: " + date, exception.Message);
        }

    }
}
