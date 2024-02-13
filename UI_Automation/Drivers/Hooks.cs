using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;
using UI_Automation.Variables;

namespace UI_Automation.Drivers
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        public static Appsettings appsettings;
        static string frameworkDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void SetupSettings()
        {
            appsettings = new Appsettings();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(GetFilePathForTest("appsettings.json", frameworkDirectory));
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
                    _container.RegisterInstanceAs(chromeDriver);
                    break;

                case "edge":
                    IWebDriver edgeDriver = new EdgeDriver(Environment.CurrentDirectory);
                    edgeDriver.Manage().Window.Maximize();
                    _container.RegisterInstanceAs(edgeDriver);
                    break;

                default:
                    throw new ArgumentException($"Browser not yet implemented, check your settings");
            }
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
