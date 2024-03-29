using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using TechTalk.SpecFlow;
using UI_Automation.Helpers;

namespace UI_Automation.Hooks
{
    [Binding]
    public class ExtentReportHook
    {
        private static AventStack.ExtentReports.ExtentReports extent;
        private static AventStack.ExtentReports.ExtentTest feature;
        private AventStack.ExtentReports.ExtentTest scenario, step;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Create a unique folder for each test run
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string reportFolder = Path.Combine(FileHelper.extentReportsDirectory, "Result", $"Result_{timestamp}");

            // Create the folder if it doesn't exist
            Directory.CreateDirectory(reportFolder);

            // Initialize the test report within the folder
            ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(Path.Combine(reportFolder, "TestReport.html"));

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
        public void AfterStep(ScenarioContext context, IWebDriver driver)
        {
            if (context.TestError == null)
            {
                step.Log(Status.Pass, context.StepContext.StepInfo.Text);
            }
            else
            {
                step.Log(Status.Fail, context.StepContext.StepInfo.Text);

                // Create a unique screenshots folder for each execution
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
                string screenshotsFolder = Path.Combine(FileHelper.extentReportsDirectory, "FailedTests", $"Failed_{timestamp}");
                Directory.CreateDirectory(screenshotsFolder);

                // Save the screenshot within the screenshots folder
                var screenshot = driver.TakeScreenshot();
                screenshot.SaveAsFile(Path.Combine(screenshotsFolder, $"Failed_{timestamp}.png"));
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            extent.Flush();
        }
    }
}
