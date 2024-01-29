using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;

namespace UI_Automation.Drivers
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        public void SetupDriver(string browser)
        {
            switch (browser)
            {
                case "chrome":
                    IWebDriver chromeDriver = new ChromeDriver(Environment.CurrentDirectory);
                    chromeDriver.Manage().Window.Maximize();
                    _container.RegisterInstanceAs(chromeDriver);
                    break;

                case "edge":
                    IWebDriver edgeDriver = new EdgeDriver(Environment.CurrentDirectory);
                    edgeDriver.Manage().Window.Maximize();
                    _container.RegisterInstanceAs(edgeDriver);
                    break;

                default:
                    throw new ArgumentException($"Browser not yet implemented: {browser}");
            }
        }

        [Given(@"I setup '([^']*)' browser")]
        public void GivenISetupBrowser(string browser)
        {
            SetupDriver(browser);
        }

        [AfterScenario]
        public void CleanUp()
        {
            IWebDriver driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }
        }

    }
}
