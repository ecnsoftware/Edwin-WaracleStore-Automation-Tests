using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WaracleStore_Automation_Tests.Support;

namespace WaracleStore_Automation_Tests.Pages
{
    public class WaracleStorePage
    {
        private readonly IPage page;
        public WaracleStorePage(IPage page)
        {
            this.page = page;
        }

        private ILocator ProductCard(string productName) => page.Locator("a.group").Filter(new LocatorFilterOptions { HasText = productName });
        private ILocator AddToCartButton(string productName) => ProductCard(productName).GetByRole(AriaRole.Button, new() { Name = "Add to Cart" });
        
        public async Task AddAnItemToCart(Table table)
        {
            var products = table.CreateSet<ProductModel>();
            foreach (var product in products)
            {
                await SelectNavigationCategory(product.Gender);
                for (int i = 0; i < product.Quantity; i++)
                {                  
                    await ProductCard(product.ItemName).HoverAsync();
                    await AddToCartButton(product.ItemName).ClickAsync();
                }         
            }
        }

        public async Task VerifyHomePage()
        {
          await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Waracle home", Exact = true })).ToBeVisibleAsync();
        }

        private async Task SelectNavigationCategory(string navCat)
        {
            await page.GetByRole(AriaRole.Navigation).GetByRole(AriaRole.Link, new() { Name = navCat, Exact = true }).ClickAsync();
        }
    }
}
