using TechTalk.SpecFlow;
using UI_Automation.POM;

namespace UI_Automation.StepDefinitions
{
    [Binding]
    public class ProductsSteps
    {
        private ProductsPage _productsPage;

        public ProductsSteps(ProductsPage productsPage)
        {
            _productsPage = productsPage;
        }

        [Then(@"I see '([^']*)' list")]
        public void ThenISeeList(string title)
        {
            _productsPage.VerifyListTitle(title)
                .VerifyProductsListIsDisplayed();   
        }
    }
}
