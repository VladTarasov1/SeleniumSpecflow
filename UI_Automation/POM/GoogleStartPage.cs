using UI_Automation.POM.Common;

namespace UI_Automation.POM
{
    public class GoogleStartPage : CommonLocators
    {
        private GoogleStartPageMethods _googleStartPageMethods;

        public GoogleStartPage(GoogleStartPageMethods googleStartPageMethods)
        {
            _googleStartPageMethods = googleStartPageMethods;
        }

        public GoogleStartPage NavigateToStartPage()
        {
            _googleStartPageMethods.Navigate("https://www.google.com/");
            return this;
        }

    }
}
