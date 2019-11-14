using OpenQA.Selenium.Chrome;
using System;

namespace CreepyCrawly.SeleniumExecutionEngine
{
    public class SeleniumExecutionEngine
    {
        public static bool DriverRunning { get; private set; } = false;
        public static ChromeDriver Driver { get; private set; }
        public static void StartDriver(string rootUrl)
        {
            string sanitizedRootUrl = rootUrl.StartsWith("http://") || rootUrl.StartsWith("https://") ? rootUrl : "http://" + rootUrl;
            try
            {
                Driver = new ChromeDriver("./");
                Driver.Manage().Window.Maximize();
                Driver.Navigate().GoToUrl(sanitizedRootUrl);
                DriverRunning = true;
            }
            catch (Exception e)
            {
                DriverRunning = false;
            }

        }

        public static void StopDriver()
        {
            try
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver.Dispose();
                    DriverRunning = false;
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
