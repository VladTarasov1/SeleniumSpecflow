using UI_Automation.POM;

namespace UI_Automation.StepDefinitions
{
    [Binding]
    public class BasicSteps
    {
        private GoogleStartPage _googleStartPage;

        public BasicSteps(GoogleStartPage googleStartPage)
        {
            _googleStartPage = googleStartPage;
        }

        [Given(@"I am on base page")]
        public void GivenIAmOnBasePage()
        {
            _googleStartPage.NavigateToStartPage();
        }

    }
}
