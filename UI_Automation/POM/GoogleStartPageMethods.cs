using OpenQA.Selenium;
using UI_Automation.POM.Common;

namespace UI_Automation.POM
{
    public class GoogleStartPageMethods : CommonMethods
    {
        private IWebDriver _webDriver;

        public GoogleStartPageMethods(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }   
    }
}
