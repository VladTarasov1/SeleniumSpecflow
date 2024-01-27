using OpenQA.Selenium;

namespace UI_Automation.POM
{
    public class GoogleStartPage
    {

        private IWebDriver _driver;

        public GoogleStartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public GoogleStartPage NavigateToStartPage()
        {
            _driver.Navigate().GoToUrl("https://www.google.com/");
            Thread.Sleep(2000);
            return this;
        }

    }
}
