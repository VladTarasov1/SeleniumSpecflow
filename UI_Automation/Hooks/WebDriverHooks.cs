using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
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
    public class WebDriverHooks
    {
        private readonly IObjectContainer container;
        
        public static Appsettings appsettings;

        public static AventStack.ExtentReports.ExtentReports extent;
        public static AventStack.ExtentReports.ExtentTest feature;
        public AventStack.ExtentReports.ExtentTest scenario, step;

        public WebDriverHooks(IObjectContainer container)
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
                    IWebDriver chromeDriver = new ChromeDriver(Environment.CurrentDirectory);
                    chromeDriver.Manage().Window.Maximize();
                    container.RegisterInstanceAs(chromeDriver);
                    break;

                case "edge":
                    IWebDriver edgeDriver = new EdgeDriver(Environment.CurrentDirectory);
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

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(FileHelper.frameworkDirectory);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReport);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            feature = extent.CreateTest(context.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            scenario = feature.CreateNode(context.ScenarioInfo.Title);
        }

        [BeforeStep]
        public void BeforeStep()
        {
            step = scenario;
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            if(context.TestError == null)
            {
                step.Log(Status.Pass, context.StepContext.StepInfo.Text);
            }
            else
            {
                step.Log(Status.Fail, context.StepContext.StepInfo.Text);
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            extent.Flush();
        }
    }
}
