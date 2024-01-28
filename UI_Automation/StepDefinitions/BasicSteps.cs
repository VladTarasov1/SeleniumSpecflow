using UI_Automation.POM;
using TechTalk.SpecFlow;
using OpenQA.Selenium;

namespace UI_Automation.StepDefinitions
{
    [Binding]
    public class BasicSteps
    {
        private GoogleStartPage _googleStartPage;
        private IWebDriver _driver;

        public BasicSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I am on base page")]
        public void GivenIAmOnBasePage()
        {
            _googleStartPage = new GoogleStartPage(_driver);
            _googleStartPage.NavigateToStartPage();
        }

    }
}
