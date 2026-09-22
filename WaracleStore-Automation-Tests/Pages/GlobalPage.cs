using Microsoft.Playwright;
using WaracleStore_Automation_Tests.Support;

namespace WaracleStore_Automation_Tests.Pages
{
    public class GlobalPage
    {
        private readonly ConfigurationProvider configurationProvider;
        private readonly IPage page;

        public GlobalPage(ConfigurationProvider configurationProvider, IPage page)
        {
            this.configurationProvider = configurationProvider;
            this.page = page;
        }

        private ILocator LoginLink => page.GetByRole(AriaRole.Button, new() { Name = "Account", Exact = true });
        private ILocator UsernameInput => page.GetByLabel("Email");
        private ILocator PasswordInput => page.Locator("#password");
        private ILocator LoginButton => page.GetByRole(AriaRole.Button, new() { Name = "Sign In" });

        public async Task NavigateToBaseUrlAsync()
        {
            var baseUrl = configurationProvider.GetBaseUrl();
            await page.GotoAsync(baseUrl);
        }

        public async Task LoginToAccount()
        
        {
            var loginDetails = configurationProvider.GetLoginCredentials();
            await LoginLink.ClickAsync();
            await UsernameInput.FillAsync(loginDetails.Username);
            await PasswordInput.FillAsync(loginDetails.Password);
            await LoginButton.ClickAsync();
        }
        
    }
}
