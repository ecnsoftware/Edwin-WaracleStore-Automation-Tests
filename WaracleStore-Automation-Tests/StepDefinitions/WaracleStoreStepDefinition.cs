using WaracleStore_Automation_Tests.Pages;

namespace WaracleStore_Automation_Tests.StepDefinitions
{
    [Binding]
    public class WaracleStoreStepDefinition
    {     
        private readonly GlobalPage globalPage;
        private readonly WaracleStorePage waracleStorePage;
        private readonly CartPage cartPage;
        public WaracleStoreStepDefinition(GlobalPage globalPage, WaracleStorePage waracleStorePage, CartPage cartPage)
        {
            this.globalPage = globalPage;
            this.waracleStorePage = waracleStorePage;
            this.cartPage = cartPage;
        }

        [Given("I navigate to Waracle Store")]
        public async Task GivenINavigateToWaracleStore()
        {
            await globalPage.NavigateToBaseUrlAsync();
        }

        [When("I add an item to the cart")]
        public async Task WhenIAddAnItemToTheCart(DataTable dataTable)
        {
          await waracleStorePage.AddAnItemToCart(dataTable);
        }

        [When("I apply the coupon code {string}")]
        public async Task WhenIApplyTheCouponCode(string couponCode)
        {
            await cartPage.ApplyCouponCode(couponCode);
        }

        [Then("the discount should be applied with the message {string}")]
        public async Task ThenTheDiscountShouldBeAppliedWithTheMessage(string message)
        {
            await cartPage.VerifyDiscountAppliedMessage(message);
        }

        [Then("the correct discount amount should be displayed as {string}")]
        public async Task ThenTheCorrectDiscountAmount(string discountAmount)
        {
            await cartPage.VerifyDiscountRate(discountAmount);
        }

        [Then("the shipping cost should be displayed as {string}")]
        public async Task ThenTheShippingCostShouldBeDisplayedAs(string shippingCost)
        {
            await cartPage.VerifyShippingCostFee(shippingCost);
        }

        [Then("the order total should be calculated correctly {string}")]
        public async Task ThenTheOrderTotalShouldBeCalculatedCorrectly(string discountAmount)
        {
            await cartPage.VerifyOrderTotalIsCalculatedCorrectly(discountAmount);
        }

        [Then("the discount should not be applied with the message {string}")]
        public async Task ThenTheDiscountShouldNotBeAppliedWithTheMessage(string invalidMessage)
        {
            await cartPage.VerifyDiscountIsNotApplied(invalidMessage);
        }

        [When("I login to the account")]
        public async Task WhenILoginToTheAccount()
        {
            await globalPage.LoginToAccount();
        }

        [Then("I should be on the home page")]
        public async Task ThenIShouldBeOnTheHomePage()
        {
            await waracleStorePage.VerifyHomePage();
        }


    }
}
