using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CreepyCrawly.SeleniumExecutionEngine
{

    public class SeleniumExecutionEngine : IDisposable
    {
        private Stack<string> _WindowContextStack = new Stack<string>();
        private Stack<Queue<IWebElement>> _ForEachIteratorStack = new Stack<Queue<IWebElement>>();
        private SeleniumExecutionDriver _ExecutionDriver;
        public bool IsEngineOk { get; private set; } = false;

        public SeleniumExecutionEngine()
        {
            _ExecutionDriver = new SeleniumExecutionDriver();
            if (!_ExecutionDriver.IsDriverRunning)
            {
                _ExecutionDriver.StartDriver("");
                IsEngineOk = true;
            }
        }

        public void StopEngine()
        {
            _ExecutionDriver.StopDriver();
        }

        public void Dispose()
        {
            StopEngine();
        }
        #region SIMPLE METHODS
        public object OnRoot(string wwwUrl)
        {
            _ExecutionDriver.Driver.Navigate().GoToUrl(wwwUrl);
            return null;
        }
        public object Click(string selector)
        {
            var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            element.Click();
            return null;
        }
        public object Submit(string selector)
        {
            var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            element.Submit();
            return null;
        }
        public object Input(string selector, string inputValue)
        {
            var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            element.Clear();
            element.SendKeys(inputValue);
            return null;
        }

        public object Select(string selector, int optionIndex)
        {
            var element = new SelectElement(_ExecutionDriver.Driver.FindElementByCssSelector(selector));
            element.SelectByIndex(optionIndex);
            return null;
        }

        public object WaitMs(int waitAmount)
        {
            System.Threading.Thread.Sleep(waitAmount);
            return null;
        }
        public object WaitLoad(string selector, int waitAmount)
        {
            new WebDriverWait(_ExecutionDriver.Driver, new TimeSpan(0, 0, 0, 0, waitAmount))
                    .Until(_ => By.CssSelector(selector));
            return null;
        }
        public string ExtractText(string selector)
        {
            var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            return element.Text;
        }
        public string ExtractTitle()
        {
            var title = _ExecutionDriver.Driver.Title;
            return title;
        }

        public string[] ExtractAllImages(string selector)
        {
            string scriptText = "var b64List = [];" +
                                   "var c = document.createElement('canvas');" +
                                   "var ctx = c.getContext('2d');" +
                                   string.Format("var imgs = document.querySelectorAll('{0}');", selector) +
                                   "imgs.forEach(function(item, index){" +
                                   "c.height=item.naturalHeight;" +
                                   "c.width=item.naturalWidth;" +
                                   "ctx.drawImage(item, 0, 0,item.naturalWidth, item.naturalHeight);" +
                                   "var uri = c.toDataURL('image/png')," +
                                   "b64 = uri.replace(/^data:image.+;base64,/, '');" +
                                   "b64List.push(b64);" +
                                   "});" +
                                   "return b64List;";
            var base64strings = _ExecutionDriver.Driver.ExecuteScript(scriptText) as string[];
            return base64strings;
        }
        public string ExtractImage(string selector)
        {
            //var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            string scriptText = string.Format(@"
                                    var c = document.createElement('canvas');
                                    var ctx = c.getContext('2d');
                                    var img = document.querySelector('{0}');
                                    c.height=img.naturalHeight;
                                    c.width=img.naturalWidth;
                                    ctx.drawImage(img, 0, 0,img.naturalWidth, img.naturalHeight);
                                    var uri = c.toDataURL('image/png'),
                                    b64 = uri.replace(/^data:image.+;base64,/, '');
                                    return b64;
                                    ", selector);
            var base64string = _ExecutionDriver.Driver.ExecuteScript(scriptText) as string;
            string[] split = base64string.Split(',');
            return split[split.Length - 1];
        }
        public object ExtractScript(string selector)
        {
            return null;
        }
        #endregion

        #region FOREACH CLICK
        public object ForEachClick_Head(string selector)
        {
            _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
            _ExecutionDriver.OpenNewDuplicateTab();
            _ForEachIteratorStack.Push(new Queue<IWebElement>(_ExecutionDriver.Driver.FindElementsByCssSelector(selector)));
            return null;
        }

        public object ForEachClick_IterationBegin()
        {
            var elementQueue = _ForEachIteratorStack.Pop();
            IWebElement element = null;
            elementQueue.TryDequeue(out element);
            
            _ForEachIteratorStack.Push(elementQueue);

            if (element != null)
            {
                _ExecutionDriver.SwitchToLastTab();
                new Actions(_ExecutionDriver.Driver)
                    .KeyDown(Keys.LeftControl)
                    .KeyDown(Keys.LeftShift)
                    .Click(element)
                    .KeyUp(Keys.LeftControl)
                    .KeyUp(Keys.LeftShift)
                    .Build()
                    .Perform();


                _ExecutionDriver.SwitchToLastTab();

                return 1;
            }
            else
            {
                return null;
            }



        }

        public object ForEachClick_IterationEnd()
        {
            _ExecutionDriver.CloseCurrentTab();
            return null;
        }

        public object ForEachClick_Tail()
        {
            string stackedTab = _WindowContextStack.Pop();
            _ForEachIteratorStack.Pop();
            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            return null;
        }
        #endregion
    }
}
