using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace CreepyCrawly.SeleniumExecutionEngine
{
    class SeleniumExecutionDriver
    {
        public bool IsDriverRunning { get; private set; } = false;
        public ChromeDriver Driver { get; private set; }
        public string CurrentTab { get; private set; }
        public string DriverPath { get; private set; } = "./";

        public SeleniumExecutionDriver(string driverPath) : base()
        {
            DriverPath = driverPath;
        }

        public SeleniumExecutionDriver()
        {
        }

        public void StartDriver(string rootUrl)
        {
            string sanitizedRootUrl = rootUrl.StartsWith("http://") || rootUrl.StartsWith("https://") ? rootUrl : "http://" + rootUrl;
            try
            {
                var service = ChromeDriverService.CreateDefaultService("./");
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;
                var options = new ChromeOptions();
                Driver = new ChromeDriver(service, options);
                Driver.Manage().Window.Maximize();
                Driver.Navigate().GoToUrl(sanitizedRootUrl);
                IsDriverRunning = true;
            }
            catch (Exception e)
            {
                IsDriverRunning = false;
            }

        }

        public void StopDriver()
        {
            try
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver.Dispose();
                    IsDriverRunning = false;
                }
            }
            catch (Exception e)
            {
            }
        }

        public void OpenNewDuplicateTab()
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
        public void CloseTabWithHandle(string tabHandle)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.IndexOf(tabHandle)]);
            Driver.Close();
        }
        public void CloseCurrentTab()
        {
            Driver.SwitchTo().Window(Driver.CurrentWindowHandle).Close();
        }
        public void SwitchToTabWithHandle(string tabHandle)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.IndexOf(tabHandle)]);
        }

        public void SwitchToSecondToLastTab()
        {
            if (Driver.WindowHandles.Count >= 2)
            {
                Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.Count - 2]);
            }
        }

        public void SwitchToLastTab()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.Count - 1]);
        }
    }
}
