using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CreepyCrawly.SeleniumExecutionEngine
{

    public class SeleniumExecutionMethods
    {
        private static Stack<string> WindowContextStack = new Stack<string>();
        private static Stack<Queue<IWebElement>> ForEachIteratorStack = new Stack<Queue<IWebElement>>();
        public static object Click(string selector)
        {
            var element = SeleniumExecutionEngine.Driver.FindElementByCssSelector(selector);
            element.Click();
            return null;
        }
        public static object Submit(string selector)
        {
            var element = SeleniumExecutionEngine.Driver.FindElementByCssSelector(selector);
            element.Submit();
            return null;
        }
        public static object Input(string selector, string inputValue)
        {
            var element = SeleniumExecutionEngine.Driver.FindElementByCssSelector(selector);
            element.Clear();
            element.SendKeys(inputValue);
            return null;
        }

        public static object Select(string selector, int optionIndex)
        {
            var element = new SelectElement(SeleniumExecutionEngine.Driver.FindElementByCssSelector(selector));
            element.SelectByIndex(optionIndex);
            return null;
        }

        public static object Wait(int waitAmount)
        {
            System.Threading.Thread.Sleep(waitAmount);
            return null;
        }
        public static object WaitLoad(string selector, int waitAmount)
        {
            new WebDriverWait(SeleniumExecutionEngine.Driver, new TimeSpan(0, 0, 0, 0, waitAmount))
                    .Until(_ => By.CssSelector(selector));
            return null;
        }
        public static string Extract(string selector)
        {
            var element = SeleniumExecutionEngine.Driver.FindElementByCssSelector(selector);
            return element.Text;
        }
        public static object ExtractScript(string selector)
        {
            return null;
        }

        #region FOREACH
        public static object ForEachHead(string selector)
        {
            WindowContextStack.Push(SeleniumExecutionEngine.Driver.CurrentWindowHandle);
            SeleniumExecutionEngine.OpenNewDuplicateTab();
            ForEachIteratorStack.Push(new Queue<IWebElement>(SeleniumExecutionEngine.Driver.FindElementsByCssSelector(selector)));
            return null;
        }

        public static object ForEachIterationBegin()
        {
            var elementQueue = ForEachIteratorStack.Pop();
            IWebElement element = elementQueue.Dequeue();
            ForEachIteratorStack.Push(elementQueue);
            
            if (element != null)
            {
                SeleniumExecutionEngine.SwitchToLastTab();
                System.Threading.Thread.Sleep(500);
                new Actions(SeleniumExecutionEngine.Driver)
                    .KeyDown(Keys.LeftControl)
                    .KeyDown(Keys.LeftShift)
                    .Click(element)
                    .KeyUp(Keys.LeftControl)
                    .KeyUp(Keys.LeftShift)
                    .Build()
                    .Perform();
                               

                SeleniumExecutionEngine.SwitchToLastTab();
                
                Console.WriteLine(SeleniumExecutionEngine.Driver.Title);
                return 1;
            }
            else
            {
                return null;
            }



        }

        public static object ForEachIterationEnd()
        {
            SeleniumExecutionEngine.CloseCurrentTab();
            return null;
        }

        public static object ForEachTail()
        {
            string stackedTab = WindowContextStack.Pop();
            ForEachIteratorStack.Pop();
            SeleniumExecutionEngine.CloseCurrentTab();
            SeleniumExecutionEngine.SwitchToTabWithHandle(stackedTab);
            return null;
        }
        #endregion
    }
}
