﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CreepyCrawly.SeleniumExecutionEngine
{

    public class SeleniumExecutionEngine
    {
        private Stack<string> _WindowContextStack = new Stack<string>();
        private Stack<Queue<IWebElement>> _ForEachIteratorStack = new Stack<Queue<IWebElement>>();
        private SeleniumExecutionDriver _ExecutionDriver;
        public bool IsEngineOk { get; private set; } = false;
        public SeleniumExecutionEngine(string rootUrl)
        {
            _ExecutionDriver = new SeleniumExecutionDriver(rootUrl);
            if (!_ExecutionDriver.IsDriverRunning)
            {
                _ExecutionDriver.StartDriver(rootUrl);
                IsEngineOk = true;
            }
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

        public object Wait(int waitAmount)
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
        public string Extract(string selector)
        {
            var element = _ExecutionDriver.Driver.FindElementByCssSelector(selector);
            return element.Text;
        }
        public object ExtractScript(string selector)
        {
            return null;
        }

        #region FOREACH
        public object ForEachHead(string selector)
        {
            _WindowContextStack.Push(_ExecutionDriver.Driver.CurrentWindowHandle);
            _ExecutionDriver.OpenNewDuplicateTab();
            _ForEachIteratorStack.Push(new Queue<IWebElement>(_ExecutionDriver.Driver.FindElementsByCssSelector(selector)));
            return null;
        }

        public object ForEachIterationBegin()
        {
            var elementQueue = _ForEachIteratorStack.Pop();
            IWebElement element = elementQueue.Dequeue();
            _ForEachIteratorStack.Push(elementQueue);

            if (element != null)
            {
                _ExecutionDriver.SwitchToLastTab();
                System.Threading.Thread.Sleep(500);
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

        public object ForEachIterationEnd()
        {
            _ExecutionDriver.CloseCurrentTab();
            return null;
        }

        public object ForEachTail()
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
