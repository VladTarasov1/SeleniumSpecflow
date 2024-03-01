namespace UI_Automation.Helpers
{
    public static class FileHelper
    {
        public static string frameworkDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName;
        public static string extentReportsDirectory = frameworkDirectory + "\\UI_Automation\\ExtentReports\\";

        public static string GetFilePathForTest(string fileName, string frameworkDirectory)
        {
            var files = Directory.GetFiles(frameworkDirectory);

            if (files.Any(file => file.EndsWith(fileName)))
            {
                return files.Single(file => file.EndsWith(fileName));
            }

            var directories = Directory.GetDirectories(frameworkDirectory);

            foreach (var directory in directories)
            {
                var subFiles = Directory.GetFiles(directory);

                if (subFiles.Any(file => file.EndsWith(fileName)))
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
