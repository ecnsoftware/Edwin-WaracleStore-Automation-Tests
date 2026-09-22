using Microsoft.Extensions.Configuration;

namespace WaracleStore_Automation_Tests.Support
{
    public class ConfigurationProvider
    {
        private readonly IConfiguration config;
        public ConfigurationProvider()
        {
            config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile($"appsettings.json", false)
             .Build();
        }

        public string GetBaseUrl()
        {
            return config.GetSection("BaseUrl").Value 
                ?? throw new InvalidOperationException("BaseUrl is missing form the config file.");
        }

        public LoginModel GetLoginCredentials()
        {
            var loginSection = config.GetSection("login").GetChildren();
            return new LoginModel
            {
                Username = loginSection.FirstOrDefault(x => x.Key == "username")?.Value
                     ?? throw new InvalidOperationException("Username is missing form the config file."),
                Password = loginSection.FirstOrDefault(x => x.Key == "password")?.Value
                     ?? throw new InvalidOperationException("Password is missing form the config file.")
            };
        }
    }
}
