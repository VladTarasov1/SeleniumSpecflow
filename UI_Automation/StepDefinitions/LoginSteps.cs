using UI_Automation.POM;
using TechTalk.SpecFlow;
using UI_Automation.Drivers;

namespace UI_Automation.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private LoginPage _loginPage;

        public LoginSteps(LoginPage loginPage)
        {
            _loginPage = loginPage;
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _loginPage.NavigateToLoginPage();
        }

        [When(@"I login using next credentials")]
        public void WhenILoginUsingNextCredentials(Table credentials)
        {
            _loginPage.EnterUsername(credentials.Rows[0]["username"])
                .EnterPassword(credentials.Rows[0]["password"])
                .ClickLoginButton();
        }
    }
}
