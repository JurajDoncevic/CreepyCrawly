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

        private bool _RunHeadless = false;
        private bool _DisableWebSecurity = false;
        private ChromeOptions _ChromeOptions;

        public SeleniumExecutionDriver(string driverPath, bool runHeadless = false, bool disableWebSecurity = false) : this(driverPath)
        {
            _RunHeadless = runHeadless;
            _DisableWebSecurity = disableWebSecurity;
            SetOptions();
        }
        public SeleniumExecutionDriver(string driverPath) : this()
        {
            DriverPath = driverPath;
            SetOptions();
        }
        private SeleniumExecutionDriver()
        {
            SetOptions();
        }
        private void SetOptions()
        {
            _ChromeOptions = new ChromeOptions();
            if (_RunHeadless)
                _ChromeOptions.AddArgument("headless");
            if (_DisableWebSecurity)
                _ChromeOptions.AddArgument("disable-web-security");
        }

        public void StartDriver()
        {
            try
            {
                string chromedriverName = new Uri(DriverPath).LocalPath;
                string pathToDriverDir = new Uri(new Uri(DriverPath), ".").AbsolutePath;
                var service = ChromeDriverService.CreateDefaultService(DriverPath, chromedriverName);
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;
                Driver = new ChromeDriver(service, _ChromeOptions);
                Driver.Manage().Window.Maximize();
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
        public void OpenNewEmptyTab()
        {
            Driver.ExecuteScript("window.open()");
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.Count - 1]);
        }

        public void OpenNewTabOnUrl(string url)
        {
            Driver.ExecuteScript("window.open()");
            Driver.SwitchTo().Window(Driver.WindowHandles[Driver.WindowHandles.Count - 1]);
            Driver.Url = url;
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
            SwitchToLastTab();
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
