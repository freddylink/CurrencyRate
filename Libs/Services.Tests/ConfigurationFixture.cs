using Microsoft.Extensions.Configuration;

namespace Services.Tests
{
    public class IConfigurationFixture
    {
        public IConfiguration Configuration { get; }

        public IConfigurationFixture()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            Configuration = configurationBuilder.Build();
        }
    }
}
