using OpenQA.Selenium;

namespace UI_Automation.POM.Common
{
    public class CommonMethods
    {
        private IWebDriver _webDriver;

        public CommonMethods(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void ClickButton(string locator)
        {
            _webDriver.FindElement(By.XPath(locator)).Click();
        }

        public void Navigate(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }
    }
}
