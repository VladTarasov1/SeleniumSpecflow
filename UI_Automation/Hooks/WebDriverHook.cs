using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;
using UI_Automation.Helpers;
using UI_Automation.Variables;

namespace UI_Automation.Drivers
{
    [Binding]
    public class WebDriverHook
    {
        private readonly IObjectContainer container;
        
        public static Appsettings appsettings;

        public WebDriverHook(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario]
        public void SetupSettings()
        {
            appsettings = new Appsettings();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(FileHelper.GetFilePathForTest("appsettings.json", FileHelper.frameworkDirectory));
            IConfigurationRoot configuration = builder.Build();
            configuration.Bind(appsettings);
        }

        [BeforeScenario]
        public void SetupDriver()
        {
            switch (appsettings.Browser)
            {
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments(appsettings.Headless);
                    IWebDriver chromeDriver = new ChromeDriver(chromeOptions);
                    chromeDriver.Manage().Window.Maximize();
                    container.RegisterInstanceAs(chromeDriver);
                    break;

                case "edge":
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments(appsettings.Headless);
                    IWebDriver edgeDriver = new EdgeDriver(edgeOptions);
                    edgeDriver.Manage().Window.Maximize();
                    container.RegisterInstanceAs(edgeDriver);
                    break;

                default:
                    throw new ArgumentException($"Browser not yet implemented, check your settings");
            }
        }

        [AfterScenario]
        public void CleanUp()
        {
            IWebDriver driver = container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
