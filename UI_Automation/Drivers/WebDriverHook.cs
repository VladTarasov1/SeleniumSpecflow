using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UI_Automation.Drivers
{
    [Binding]
    public class WebDriverHook
    {
        private IWebDriver _webDriver;

        public WebDriverHook(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [BeforeScenario]
        public void SetupDriver()
        {
            _webDriver = new ChromeDriver(Environment.CurrentDirectory);
        }

        [AfterScenario]
        public void CleanUp()
        {
            _webDriver.Quit();
        }

    }
}
