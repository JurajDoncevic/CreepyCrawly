using System;
using System.Collections.Generic;

namespace CreepyCrawly.Core
{
    /// <summary>
    /// Interface to describe CrawlLang commands and their sub-actions that should be implemented in an ExecutionEngine class.
    /// An ExecutionEngine class should use Stacks and Queues (if possible) to store contexts in an orderly fashion.
    /// </summary>
    public interface IExecutionEngine
    {
        #region CONTROL METHODS
        /// <summary>
        /// Is the engine started and running
        /// </summary>
        bool IsEngineOk { get; }
        /// <summary>
        /// Starts the execution engine
        /// </summary>
        void StartEngine();
        /// <summary>
        /// Stop the execution engine
        /// </summary>
        void StopEngine();
        #endregion

        #region SIMPLE COMMANDS
        /// <summary>
        /// CLICK command implementation. Clicks on an element with a given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>null</returns>
        object Click(string selector);
        /// <summary>
        /// INPUT command implementation. Inputs a value in an element with a given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <param name="inputValue">Value to be inputted</param>
        /// <returns>null</returns>
        object Input(string selector, string inputValue);
        /// <summary>
        /// ON ROOT command implementation. Determines the root URL at which the script starts executing.
        /// </summary>
        /// <param name="wwwUrl">Starting URL</param>
        /// <returns>null</returns>
        object OnRoot(string wwwUrl);
        /// <summary>
        /// SELECT command implementation. Selects a value from a dropdown list with a given selector.
        /// </summary>
        /// <param name="selector">Dropdown list selector</param>
        /// <param name="optionIndex">0-index of selection</param>
        /// <returns></returns>
        object Select(string selector, int optionIndex);
        /// <summary>
        /// SUBMIT command implementation. Submits an element (form or input) with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>null</returns>
        object Submit(string selector);
        /// <summary>
        /// WAIT_FOR command implementation. Waits an amount of time for an element with given selector to appear/load.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <param name="waitAmount">Amount of time to wait in milliseconds</param>
        /// <returns>null</returns>
        object WaitFor(string selector, int waitAmount);
        /// <summary>
        /// WAIT_MS command implementation. Waits an amount of time.
        /// </summary>
        /// <param name="waitAmount">Amount of time to wait in milliseconds</param>
        /// <returns>null</returns>
        object WaitMs(int waitAmount);
        /// <summary>
        /// EXTRACT_HREF command implementation. Extracts the href attribute from an element with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>Href string</returns>
        string ExtractHref(string selector);
        /// <summary>
        /// EXTRACT_ALL_HREFS command implementation. Extracts all href link from children of element with given selector
        /// </summary>
        /// <param name="selector"></param>
        /// <returns>CSV of href links</returns>
        string ExtractAllHrefs(string selector);
        /// <summary>
        /// EXTRACT_IMAGE command implementation. Extracts an image from an img element with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>Base64 string of picture data</returns>
        string ExtractImage(string selector);
        /// <summary>
        /// EXTRACT_SCRIPT command implementation. Extracts a JS script with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>Script string</returns>
        object ExtractScript(string selector);
        /// <summary>
        /// EXTRACT_TEXT command implementation. Extracts text from an element with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <param name="regex">Regex to match text with</param>
        /// <returns>String of text</returns>
        string ExtractText(string selector, string regex = null);
        /// <summary>
        /// EXTRACT_TITLE command implementation. Extract the (meta-tag) title from the current page. 
        /// </summary>
        /// <returns>Title string</returns>
        string ExtractTitle();
        /// <summary>
        /// EXTRACT_TO_CSV command implementation. Extracts text from a set of elements with given selectors.
        /// </summary>
        /// <param name="selectors">Collection of selector strings</param>
        /// <returns>CSV string of texts</returns>
        object ExtractToCsv(ICollection<string> selectors);
        /// <summary>
        /// EXTRACT_ALL_IMAGES command implementation. Extracts all images from the page with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>Array of base64 data strings</returns>
        string[] ExtractAllImages(string selector);
        #endregion

        #region DO_WHILE
        /// <summary>
        /// DO_WHILE_CLICK command head. Stores important context and prepares the current context for command execution.
        /// </summary>
        /// <returns>null</returns>
        object DoWhileClick_Head();
        /// <summary>
        /// DO_WHILE_CLICK command iteration begin. If needed, stores important context and prepares the current context for the next iteration of commands.
        /// </summary>
        /// <returns>null</returns>
        object DoWhileClick_IterationBegin();
        /// <summary>
        /// DO_WHILE_CLICK command iteration end. If needed, stores important context and prepares the current context for the next iteration. Clicks on the next element with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>1 on success; null on failure</returns>
        object DoWhileClick_IterationEnd(string selector);
        /// <summary>
        /// DO_WHILE_CLICK command tail. Finishes execution and reverts to the beginning context.
        /// </summary>
        /// <returns></returns>
        object DoWhileClick_Tail();
        #endregion

        #region FOREACH_CLICK
        /// <summary>
        /// FOREACH_CLICK command head. Stores important context and prepares the current context for command execution. Elements with given selector are clicked with each iteration.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>null</returns>
        object ForEachClick_Head(string selector);
        /// <summary>
        /// FOREACH_CLICK command iteration begin. If needed, stores important context and prepares the current context for the next iteration of commands. Clicks on the next element with given selector.
        /// </summary>
        /// <returns>1 on success; null on failure</returns>
        object ForEachClick_IterationBegin();
        /// <summary>
        /// FOREACH_CLICK command iteration end. If needed, stores important context and prepares the current context for the next iteration.
        /// <returns>null</returns>
        object ForEachClick_IterationEnd();
        /// <summary>
        /// FOREACH_CLICK command tail. Finishes execution and reverts to the beginning context.
        /// </summary>
        /// <returns>null</returns>
        object ForEachClick_Tail();
        #endregion

        #region FOREACH_HREF
        /// <summary>
        /// FOREACH_HREF command head. Stores important context and prepares the current context for command execution. Elements with the given selector have their href links visited with each iteration.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>null</returns>
        object ForEachHref_Head(string selector);
        /// <summary>
        /// FOREACH_HREF command iteration begin. If needed, stores important context and prepares the current context for the next iteration of commands. Visits href of the next element with given selector.
        /// </summary>
        /// <returns>1 on success; null on failure</returns>
        object ForEachHref_IterationBegin();
        /// <summary>
        /// FOREACH_HREF command iteration end. If needed, stores important context and prepares the current context for the next iteration.
        /// <returns>null</returns>
        object ForEachHref_IterationEnd();
        /// <summary>
        /// FOREACH_HREF command tail. Finishes execution and reverts to the beginning context.
        /// </summary>
        /// <returns>null</returns>
        object ForEachHref_Tail();
        #endregion

        #region FORACH_SELECT
        /// <summary>
        /// FOREACH_SELECT Head method. Saves the current context if no context of FOREACH_SELECT type exists and creates a FOREACH_SELECT type context.
        /// </summary>
        /// <param name="selector">HTML Select element selector</param>
        /// <returns>null</returns>
        public object ForEachSelect_Head(string selector);
        /// <summary>
        /// FOREACH_SELECT Iteration begin method. Initiates a new iteration over options of a select element.
        /// </summary>
        /// <param name="selector">Selector of Select element</param>
        /// <returns>1 if there are options left; else returns null</returns>
        public object ForEachSelect_IterationBegin(string selector);
        /// <summary>
        /// FOREACH_SELECT Iteration end. Finishes iteration execution.
        /// </summary>
        /// <returns>null</returns>
        public object ForEachSelect_IterationEnd();
        /// <summary>
        /// FOREACH_SELECT Tail method. Recovers saved context if it is the upmost FOREACH_SELECT command.
        /// </summary>
        /// <returns>null</returns>
        public object ForEachSelect_Tail();

        #endregion

        #region CLICK_EACH
        /// <summary>
        /// CLICK_EACH command head. Saves the current context if no context of CLICK_EACH type exists and creates a CLICK_EACH type context.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>null</returns>
        object ClickEach_Head(string selector);
        /// <summary>
        /// CLICK_EACH command iteration begin. If needed, stores important context and prepares the current context for the next iteration of commands. Clicks on the next element with given selector.
        /// </summary>
        /// <returns>1 on success; null on failure</returns>
        object ClickEach_IterationBegin();
        /// <summary>
        /// CLICK_EACH command iteration end. If needed, stores important context and prepares the current context for the next iteration.
        /// <returns>null</returns>
        object ClickEach_IterationEnd();
        /// <summary>
        /// CLICK_EACH command tail. Finishes execution and reverts to the beginning context.
        /// </summary>
        /// <returns>null</returns>
        object ClickEach_Tail();
        #endregion

        #region GOTO_CLICK
        /// <summary>
        /// GOTO_CLICK command head. Stores context and clicks on a element with given selector.
        /// </summary>
        /// <param name="selector">Selector string</param>
        /// <returns>null</returns>
        object GotoClick_Head(string selector);
        /// <summary>
        /// GOTO_CLICK command tail. Finishes command execution and recovers old context.
        /// </summary>
        /// <returns>null</returns>
        object GotoClick_Tail();
        #endregion

        #region WHILE_CLICK
        /// <summary>
        /// WHILE_CLICK command head. Stores important context and prepares the current context for command execution. 
        /// </summary>
        /// <returns>null</returns>
        object WhileClick_Head();
        /// <summary>
        /// WHILE_CLICK command iteration begin. If needed, stores important context and prepares the current context for the next iteration of commands. Clicks on the next element with given selector.
        /// </summary>
        /// <returns>1 on success; null on failure</returns>
        object WhileClick_IterationBegin(string selector);
        /// <summary>
        /// WHILE_CLICK command iteration end. If needed, stores important context and prepares the current context for the next iteration.
        /// <returns>null</returns>
        object WhileClick_IterationEnd();
        /// <summary>
        /// WHILE_CLICK command tail. Finishes execution and reverts to the beginning context.
        /// </summary>
        /// <returns>null</returns>
        object WhileClick_Tail();
        #endregion
    }
}
