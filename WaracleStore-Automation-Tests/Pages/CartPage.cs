using Io.Cucumber.Messages.Types;
using Microsoft.Extensions.Primitives;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaracleStore_Automation_Tests.Support;

namespace WaracleStore_Automation_Tests.Pages
{
    public class CartPage
    {
        private readonly IPage page;
        public CartPage(IPage page)
        {
            this.page = page;
        }
        private ILocator CartLink => page.GetByRole(AriaRole.Link, new() { Name = "Cart", Exact = true });
        private ILocator CouponInput => page.GetByPlaceholder("e.g. WARACLE25");
        private ILocator ApplyButton => page.GetByRole(AriaRole.Button, new() { Name = "Apply" });
        private ILocator DiscountText(string code) => page.GetByText($"Coupon ({code})", new() { Exact = true }).Locator("xpath=following-sibling::span");//not ideal
        private ILocator TotalText(string label) => page.GetByText(label, new() { Exact = true }).Locator("xpath=following-sibling::span"); //not ideal
        private ILocator ShippingValue => page.GetByText("Shipping", new() { Exact = true }).Locator("..").Locator("span").Last; //Not ideal
        public async Task VerifyItemIsInCart(Table table)
        {
            var productNames = table.CreateSet<ProductModel>();
            await CartLink.ClickAsync();

            foreach (var product in productNames)
            {
                await Assertions.Expect(page.GetByText(product.ItemName, new() { Exact = true })).ToBeVisibleAsync();
            }

        }

        public async Task ApplyCouponCode(string couponCode)
        {
            await CartLink.ClickAsync();
            await CouponInput.FillAsync(couponCode);
            await ApplyButton.ClickAsync();
        }

        public async Task VerifyDiscountAppliedMessage(string message)
        {
            var discountMessage = page.GetByText(message, new() { Exact = true });
            await Assertions.Expect(discountMessage).ToBeVisibleAsync();
        }

        public async Task VerifyDiscountIsAppliedToSubtotal(string percentage)
        {
            decimal ExpectedDiscountRate = decimal.Parse(percentage.TrimEnd('%')) / 100m;
            var subtotalAmount = await GetSubtotalAmount();
            var actualDiscountAmount = await GetCouponDiscountAmount("WARACLE25");
            var expectedDiscount = subtotalAmount * ExpectedDiscountRate;

            Assert.That(actualDiscountAmount, Is.EqualTo(expectedDiscount));
            //await Assertions.Expect(subtotalElement).ToBeVisibleAsync();
        }

        public async Task VerifyDiscountRate(string percentage)
        {
            var actualDiscountAmount = await GetCouponDiscountAmount("WARACLE25");
            var expectedDiscount = await GetExpectedDiscountRate(percentage);

            Assert.That(actualDiscountAmount, Is.EqualTo(expectedDiscount));
            await Assertions.Expect(page.GetByText($"-£{expectedDiscount:F2}", new() { Exact = true })).ToBeVisibleAsync();
        }

        public async Task VerifyShippingCostFee(string expectedShippingCost)
        {
            await CartLink.ClickAsync();
            var shippingText = await ShippingValue.InnerTextAsync();
            Assert.That(shippingText, Is.EqualTo(expectedShippingCost));
        }

        public async Task VerifyOrderTotalIsCalculatedCorrectly(string percentage)
        {
            var subtotalAmount = await GetSubtotalAmount();
            var expectedDiscount = await GetExpectedDiscountRate(percentage);
            var shippingCost = await GetShippingCost();
            var expectedTotal = (subtotalAmount - expectedDiscount) + shippingCost;
            var actualTotal = await GetTotalAmount();
            Assert.That(actualTotal, Is.EqualTo(expectedTotal));
            await Assertions.Expect(page.GetByText($"£{expectedTotal:F2}", new() { Exact = true })).ToBeVisibleAsync();
        }

        public async Task VerifyDiscountIsNotApplied(string invalidMessage)
        {
            var discountMessage = page.GetByText(invalidMessage, new() { Exact = true });
            await Assertions.Expect(discountMessage).ToBeVisibleAsync();
        }

        private async Task<decimal> GetExpectedDiscountRate(string percentage)
        {
            decimal ExpectedDiscountRate = decimal.Parse(percentage.TrimEnd('%')) / 100m;
            var subtotalAmount = await GetSubtotalAmount();
            var expectedDiscount = subtotalAmount * ExpectedDiscountRate;
            return expectedDiscount;
        }
        private async Task<decimal> GetSubtotalAmount()
        {
            var subtotalText = await TotalText("Subtotal").InnerTextAsync();
            return decimal.Parse(subtotalText
                .Replace("£", "")
                .Trim());
        }

        private async Task<decimal> GetTotalAmount()
        {
            var totalText = await TotalText("Total").InnerTextAsync();
            return decimal.Parse(totalText
                .Replace("£", "")
                .Trim());
        }

        public async Task<decimal> GetShippingCost()
        {
            var shippingText = await TotalText("Shipping").InnerTextAsync();
            return decimal.Parse(shippingText
                .Replace("£", "")
                .Trim());
        }
        private async Task<decimal> GetCouponDiscountAmount(string couponCode)
        {
            var discountText = await DiscountText(couponCode).InnerTextAsync();

            return decimal.Parse(discountText
                 .Replace("£", "")
    .Trim()
    .TrimStart('-', '–', '−'));
        }
    }
}
