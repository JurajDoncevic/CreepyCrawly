using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using CreepyCrawly.Core;

namespace CreepyCrawly.SeleniumExecution
{

    public class SeleniumExecutionEngine : IExecutionEngine, IDisposable
    {
        private Stack<string> _WindowContextStack = new Stack<string>();
        private Stack<Queue<IWebElement>> _ClickIterationStack = new Stack<Queue<IWebElement>>();
        private Stack<Queue<string>> _HrefIterationStack = new Stack<Queue<string>>();
        private Stack<int> _SelectIterationStack = new Stack<int>();
        private Stack<Queue<IWebElement>> _ClickEachIterationStack = new Stack<Queue<IWebElement>>();
        private SeleniumExecutionDriver _ExecutionDriver;
        public bool IsEngineOk { get; private set; } = false;

        public SeleniumExecutionEngine(string chromeDriverPath, SeleniumExecutionEngineOptions options)
        {
            _ExecutionDriver = new SeleniumExecutionDriver(chromeDriverPath, options.RunHeadlessBrowser, options.DisableWebSecurity);
        }

        public SeleniumExecutionEngine()
        {
            _ExecutionDriver = new SeleniumExecutionDriver("./");
        }

        public void StartEngine()
        {
            if (!_ExecutionDriver.IsDriverRunning)
            {
                _ExecutionDriver.StartDriver();
                IsEngineOk = _ExecutionDriver.IsDriverRunning;
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

        public object PutInnerHtml(string selector, string inputValue)
        {
            var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            _ExecutionDriver.Driver.ExecuteScript(string.Format("var ele=arguments[0]; ele.innerHTML = '{0}';", inputValue), element);
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

        public object WaitFor(string selector, int waitAmount)
        {
            new WebDriverWait(_ExecutionDriver.Driver, new TimeSpan(0, 0, 0, 0, waitAmount))
                    .Until(_ => By.CssSelector(selector));
            return null;
        }

        public string ExtractText(string selector, string regex = null)
        {
            var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            string text = element.Text;

            if(regex != null)
            {
                text = System.Text.RegularExpressions.Regex.Match(text, regex).Value;
            }
                

            return text;
        }

        public string ExtractTitle()
        {
            var title = _ExecutionDriver.Driver.Title;
            return title;
        }

        public string ExtractHref(string selector)
        {
            var href = _ExecutionDriver.Driver.FindElementByCssSelector(selector).GetAttribute("href");
            return href;
        }

        public string ExtractAllHrefs(string selector)
        {
            IWebElement parentElement = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            string hrefCsv = "";
            if(parentElement != null)
            {
                hrefCsv = parentElement.FindElements(By.CssSelector("[href]"))
                                       .ToList()
                                       .Select(_=>_.GetAttribute("href"))
                                       .Aggregate((_1, _2) => _1 + "," + _2);
            }

            return hrefCsv;
        }

        public string[] ExtractAllImages(string selector)
        {
            string scriptText = "var b64List = [];" +
                                   "var c = document.createElement('canvas');" +
                                   "var ctx = c.getContext('2d');" +
                                   string.Format("var imgs = document.querySelectorAll('{0}');", selector) +
                                   "imgs.forEach(function(item, index){" +
                                   "item.crossOrigin='anonymous';" +
                                   "c.height=item.naturalHeight;" +
                                   "c.width=item.naturalWidth;" +
                                   "ctx.drawImage(item, 0, 0,item.naturalWidth, item.naturalHeight);" +
                                   "var uri = c.toDataURL('image/png')," +
                                   "b64 = uri.replace(/^data:image.+;base64,/, '');" +
                                   "b64List.push(b64);" +
                                   "});" +
                                   "return b64List;";
            var base64strings = ((IReadOnlyCollection<object>)_ExecutionDriver.Driver.ExecuteScript(scriptText)).Select(_ => _.ToString()).ToArray();
            return base64strings;
        }
        public string ExtractImage(string selector)
        {
            string retVal = null;
            try
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
                retVal = split[split.Length - 1];
            }
            catch (Exception e)
            {
                try
                {
                    retVal = TryWithSrcExtractImage(selector);
                }
                catch
                {
                    throw;
                }
            }
            return retVal;
        }
        private string TryWithSrcExtractImage(string selector)
        {
            string retVal = null;
            _ExecutionDriver.OpenNewDuplicateTab();
            try
            {
                _ExecutionDriver.SwitchToLastTab();

                IWebElement element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
                if (element != null)
                {
                    string srcUrl = element.GetAttribute("src");
                    _ExecutionDriver.Driver.Url = srcUrl;

                    string scriptText = string.Format(@"
                                    var c = document.createElement('canvas');
                                    var ctx = c.getContext('2d');
                                    var img = document.querySelector('img');
                                    c.height=img.naturalHeight;
                                    c.width=img.naturalWidth;
                                    ctx.drawImage(img, 0, 0,img.naturalWidth, img.naturalHeight);
                                    var uri = c.toDataURL('image/png'),
                                    b64 = uri.replace(/^data:image.+;base64,/, '');
                                    return b64;
                                    ", selector);

                    var base64string = _ExecutionDriver.Driver.ExecuteScript(scriptText) as string;
                    string[] split = base64string.Split(',');
                    retVal = split[split.Length - 1];
                }
            }
            catch
            {
                _ExecutionDriver.CloseCurrentTab();
                throw;
            }
            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToLastTab();
            return retVal;
        }

        public object ExtractScript(string selector)
        {
            return null;
        }

        public object ExtractToCsv(ICollection<string> selectors)
        {
            List<IWebElement> elements = new List<IWebElement>();
            foreach (string selector in selectors)
            {
                IWebElement element = null;
                try
                {
                    element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
                }
                catch (Exception e)
                {

                }
                elements.Add(element);
            }
            string csv = elements.Select(_ => _ != null ? _.Text : "").Aggregate((_1, _2) => _1 + "," + _2);
            return csv;
        }

        #endregion

        #region FOREACH CLICK
        public object ForEachClick_Head(string selector)
        {
            _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
            _ExecutionDriver.OpenNewDuplicateTab();
            _ClickIterationStack.Push(new Queue<IWebElement>(_ExecutionDriver.Driver.FindElementsByCssSelector(selector)));
            return null;
        }

        public object ForEachClick_IterationBegin()
        {
            var elementQueue = _ClickIterationStack.Pop();
            IWebElement element = null;
            elementQueue.TryDequeue(out element);

            _ClickIterationStack.Push(elementQueue);

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
            _ClickIterationStack.Pop();
            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            return null;
        }
        #endregion

        #region FOREACH HREF
        public object ForEachHref_Head(string selector)
        {
            _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
            _ExecutionDriver.OpenNewDuplicateTab();

            List<string> hrefs = new List<string>();
            string currentUrl = _ExecutionDriver.Driver.Url;
            foreach (IWebElement element in _ExecutionDriver.Driver.FindElementsByCssSelector(selector))
            {
                hrefs.Add(element.GetAttribute("href"));
            }
            _HrefIterationStack.Push(new Queue<string>(hrefs));
            return null;
        }

        public object ForEachHref_IterationBegin()
        {
            var hrefQueue = _HrefIterationStack.Pop();
            string hrefUrl = null;
            hrefQueue.TryDequeue(out hrefUrl);

            _HrefIterationStack.Push(hrefQueue);

            if (hrefUrl != null)
            {
                _ExecutionDriver.OpenNewDuplicateTab();
                _ExecutionDriver.SwitchToLastTab();
                _ExecutionDriver.Driver.Url = hrefUrl;


                return 1;
            }
            else
            {
                return null;
            }



        }

        public object ForEachHref_IterationEnd()
        {

            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToLastTab();
            return null;
        }

        public object ForEachHref_Tail()
        {
            string stackedTab = _WindowContextStack.Pop();
            _HrefIterationStack.Pop();
            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            return null;
        }
        #endregion

        #region FOREACH SELECT
        public object ForEachSelect_Head(string selector)
        {
            //if there is no foreach_select command active in the scope, duplicate the context
            if(_SelectIterationStack.Count == 0)
            {
                _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
                _ExecutionDriver.OpenNewDuplicateTab();
            }

            SelectElement element = new SelectElement(_ExecutionDriver.Driver.FindElementByCssSelector(selector));
            int numberOfOptions = element.Options.Count;
            _SelectIterationStack.Push(numberOfOptions - 1);
            return null;
        }

        public object ForEachSelect_IterationBegin(string selector)
        {
            int optionIndex;
            if(_SelectIterationStack.Peek() >= 0 && _SelectIterationStack.TryPop(out optionIndex))
            {
                var element = new SelectElement(_ExecutionDriver.Driver.FindElementByCssSelector(selector));
                element.SelectByIndex(optionIndex);

                optionIndex--;
                _SelectIterationStack.Push(optionIndex);

                return 1;
            }

            return null;

        }

        public object ForEachSelect_IterationEnd()
        {
            return null;
        }

        public object ForEachSelect_Tail()
        {
            _SelectIterationStack.Pop();
            //if this was the foreach_select context holder - close the context
            if (_SelectIterationStack.Count == 0)
            {
                string stackedTab = _WindowContextStack.Pop();
                _ExecutionDriver.CloseCurrentTab();
                _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            }

            return null;
        }
        #endregion

        #region WHILE CLICK
        public object WhileClick_Head()
        {
            _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
            _ExecutionDriver.OpenNewDuplicateTab();
            _ClickIterationStack.Push(new Queue<IWebElement>());
            return null;
        }

        public object WhileClick_IterationBegin(string selector)
        {
            IWebElement element = null;
            try
            {
                element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);

            }
            catch (Exception)
            {
            }

            if (element != null)
            {
                _ExecutionDriver.SwitchToLastTab();
                element.Click();

                return 1;
            }
            else
            {
                return null;
            }
        }

        public object WhileClick_IterationEnd()
        {
            return null;
        }

        public object WhileClick_Tail()
        {
            string stackedTab = _WindowContextStack.Pop();
            _ClickIterationStack.Pop();
            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            return null;
        }
        #endregion

        #region DO WHILE CLICK
        public object DoWhileClick_Head()
        {
            _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
            _ExecutionDriver.OpenNewDuplicateTab();
            _ClickIterationStack.Push(new Queue<IWebElement>());
            return null;
        }

        public object DoWhileClick_IterationBegin()
        {
            return null;
        }

        public object DoWhileClick_IterationEnd(string selector)
        {
            IWebElement element = null;
            try
            {
                element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);

            }
            catch (Exception)
            {
            }

            if (element != null)
            {
                _ExecutionDriver.SwitchToLastTab();
                element.Click();

                return 1;
            }
            else
            {
                return null;
            }
        }

        public object DoWhileClick_Tail()
        {
            string stackedTab = _WindowContextStack.Pop();
            _ClickIterationStack.Pop();
            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            return null;
        }
        #endregion

        #region GOTO CLICK
        public object GotoClick_Head(string selector)
        {
            _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
            _ExecutionDriver.OpenNewDuplicateTab();
            IWebElement element = null;
            try
            {
                element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            }
            catch (Exception)
            {
                
            }
            
            element?.Click();

            return null;
        }
        public object GotoClick_Tail()
        {
            string stackedTab = _WindowContextStack.Pop();
            _ExecutionDriver.CloseCurrentTab();
            _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            return null;
        }
        #endregion

        #region CLICK EACH
        public object ClickEach_Head(string selector)
        {
            //if there is no click_each command active in the scope, duplicate the context
            if (_ClickEachIterationStack.Count == 0)
            {
                _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
                _ExecutionDriver.OpenNewDuplicateTab();
            }

            _ClickEachIterationStack.Push(new Queue<IWebElement>(_ExecutionDriver.Driver.FindElementsByCssSelector(selector)));
            
            return null;
        }

        public object ClickEach_IterationBegin()
        {
            var elementQueue = _ClickEachIterationStack.Pop();
            IWebElement element = null;
            elementQueue.TryDequeue(out element);

            _ClickEachIterationStack.Push(elementQueue);

            if (element != null)
            {
                element.Click();
                return 1;
            }
            else
            {
                return null;
            }
        }

        public object ClickEach_IterationEnd()
        {
            return null;
        }

        public object ClickEach_Tail()
        {
            _ClickEachIterationStack.Pop();
            //if this was the click_each context holder - close the context
            if (_ClickEachIterationStack.Count == 0)
            {
                string stackedTab = _WindowContextStack.Pop();
                _ExecutionDriver.CloseCurrentTab();
                _ExecutionDriver.SwitchToTabWithHandle(stackedTab);
            }

            return null;
        }
        #endregion
    }
}
