using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Security;
using TechTalk.SpecFlow;
using UI_Automation.ConfigVariables;

namespace UI_Automation.Drivers
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        public static Settings settings;
        static string settingsPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        public void SetupDriver(string browser)
        {
            switch (browser)
            {
                case "chrome":
                    IWebDriver chromeDriver = new ChromeDriver(Environment.CurrentDirectory);
                    chromeDriver.Manage().Window.Maximize();
                    _container.RegisterInstanceAs(chromeDriver);
                    break;

                case "edge":
                    IWebDriver edgeDriver = new EdgeDriver(Environment.CurrentDirectory);
                    edgeDriver.Manage().Window.Maximize();
                    _container.RegisterInstanceAs(edgeDriver);
                    break;

                default:
                    throw new ArgumentException($"Browser not yet implemented: {browser}");
            }
        }

        [BeforeScenario]
        public void SetupSettings()
        {
            settings = new Settings();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(GetFilePathForTest("settings.json", settingsPath));
            IConfigurationRoot configuration = builder.Build();
            configuration.Bind(settings);
        }

        [Given(@"I setup '([^']*)' browser")]
        public void GivenISetupBrowser(string browser)
        {
            SetupDriver(browser);
        }

        [AfterScenario]
        public void CleanUp()
        {
            IWebDriver driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }
        }

        private static string GetFilePathForTest(string fileName, string frameworkDirectory)
        {
            var files = Directory.GetFiles(frameworkDirectory);

            if(files.Any(file => file.EndsWith(fileName)))
            {
                return files.Single(file => file.EndsWith(fileName));
            }

            var directories = Directory.GetDirectories(frameworkDirectory);
            
            foreach(var directory in directories)
            {
                var subFiles = Directory.GetFiles(directory);

                if(subFiles.Any(file => file.EndsWith(fileName)))
                {
                    return subFiles.Single(file => file.EndsWith(fileName));    
                }

                var path = GetFilePathForTest(fileName, directory);

                if (!string.IsNullOrEmpty(path))
                {
                    return path;
                }
            }
            
            return string.Empty;
        }
    }
}
