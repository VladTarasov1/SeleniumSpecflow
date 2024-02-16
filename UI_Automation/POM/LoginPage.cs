using OpenQA.Selenium;
using UI_Automation.Drivers;

namespace UI_Automation.POM
{
    public class LoginPage
    {
        private string userNameField = "//input[@id='user-name']";
        private string passwordField = "//input[@id='password']";
        private string loginButton = "//input[@id='login-button']";

        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public LoginPage NavigateToLoginPage()
        {
            _driver.Navigate().GoToUrl(WebDriverHook.appsettings.BaseUrl);
            return this;
        }

        public LoginPage EnterUsername(string username)
        {
            _driver.FindElement(By.XPath(userNameField)).SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            _driver.FindElement(By.XPath(passwordField)).SendKeys(password);
            return this;
        }

        public LoginPage ClickLoginButton()
        {
            _driver.FindElement(By.XPath(loginButton)).Click();
            return this;
        }
    }
}
