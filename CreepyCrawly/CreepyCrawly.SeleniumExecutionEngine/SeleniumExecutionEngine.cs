using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace CreepyCrawly.SeleniumExecutionEngine
{
    public class SeleniumExecutionEngine
    {
        public static bool DriverRunning { get; private set; } = false;
        public static ChromeDriver Driver { get; private set; }
        public static string CurrentTab { get; private set; }
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

        public static void OpenNewDuplicateTab()
        {
            string currentUrl = Driver.Url;
            Driver.ExecuteScript("window.open()");
            //Actions actions = new Actions(Driver);
            //actions.KeyDown(Keys.Alt)
            //       .SendKeys("D")
            //       .SendKeys(Keys.Enter)
            //       .KeyUp(Keys.Alt)
            //       .Build()
            //       .Perform();
            
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.Count - 1]);
            Driver.Url = currentUrl;
        }
        public static void CloseTabWithHandle(string tabHandle)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.IndexOf(tabHandle)]);
            Driver.Close();
        }
        public static void CloseCurrentTab()
        {
            Driver.Close();
        }
        public static void SwitchToTabWithHandle(string tabHandle)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.IndexOf(tabHandle)]);
        }
    }
}
