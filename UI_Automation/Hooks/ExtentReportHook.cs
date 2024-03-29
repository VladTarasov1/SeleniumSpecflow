using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
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
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
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
        public void AfterStep(ScenarioContext context)
        {
            if (context.TestError == null)
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
