using Microsoft.Playwright;
using Reqnroll.BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaracleStore_Automation_Tests.Drivers;

namespace WaracleStore_Automation_Tests
{
    [Binding]
    public class Hook
    {
        private readonly IObjectContainer objectContainer;
        private readonly ScenarioContext scenarioContext;
        private BrowserFactory? browserDriver;

        public Hook(ScenarioContext scenarioContext, IObjectContainer objectContainer)
        {
            this.scenarioContext = scenarioContext;
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            browserDriver = new BrowserFactory();
            var page = await browserDriver.CreatePageAsync();
            scenarioContext.Set(page);
            objectContainer.RegisterInstanceAs<IPage>(page);
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (browserDriver != null)
            {
                await browserDriver.DisposeAsync();
            }
        }
    }
}
