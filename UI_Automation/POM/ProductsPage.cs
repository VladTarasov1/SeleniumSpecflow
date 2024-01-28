using NUnit.Framework;
using OpenQA.Selenium;

namespace UI_Automation.POM
{
    public class ProductsPage
    {
        private string listTitle = "//span[@class='title']";
        private string productsList = "//div[@class='inventory_list']";

        private IWebDriver _driver;

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ProductsPage VerifyListTitle(string expectedTitle)
        {
            Assert.That(_driver.FindElement(By.XPath(listTitle)).Text, Is.EqualTo(expectedTitle), 
                $"'{expectedTitle}' title should be displayed");
            return this;
        }

        public ProductsPage VerifyProductsListIsDisplayed()
        {
            Assert.That(_driver.FindElement(By.XPath(productsList)).Displayed, 
                "Products list should be displayed");
            return this;
        }
    }
}
