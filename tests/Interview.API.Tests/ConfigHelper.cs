using Microsoft.Extensions.Configuration;

namespace Interview.API.Tests
{
    internal static class ConfigHelper
    {
        public static IConfigurationRoot CreateConfig(string configFilePath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "/config")
                .AddJsonFile(configFilePath)
                .Build();
        }
    }
}
