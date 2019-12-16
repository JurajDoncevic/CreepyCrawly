using System;
using System.Collections.Generic;

namespace CreepyCrawly.Core
{
    public interface IExecutionEngine
    {
        #region CONTROL METHODS
        bool IsEngineOk { get; }
        void StartEngine();
        void StopEngine();
        #endregion

        #region SIMPLE COMMANDS
        object Click(string selector);
        object Input(string selector, string inputValue);
        object OnRoot(string wwwUrl);
        object Select(string selector, int optionIndex);
        object Submit(string selector);
        object WaitFor(string selector, int waitAmount);
        object WaitMs(int waitAmount);
        string ExtractHref(string selector);
        string ExtractImage(string selector);
        object ExtractScript(string selector);
        string ExtractText(string selector);
        string ExtractTitle();
        object ExtractToCsv(ICollection<string> selectors);
        string[] ExtractAllImages(string selector);
        #endregion

        #region DO_WHILE
        object DoWhileClick_Head();
        object DoWhileClick_IterationBegin();
        object DoWhileClick_IterationEnd(string selector);
        object DoWhileClick_Tail();
        #endregion

        #region FOREACH_CLICK
        object ForEachClick_Head(string selector);
        object ForEachClick_IterationBegin();
        object ForEachClick_IterationEnd();
        object ForEachClick_Tail();
        #endregion

        #region FOREACH_HREF
        object ForEachHref_Head(string selector);
        object ForEachHref_IterationBegin();
        object ForEachHref_IterationEnd();
        object ForEachHref_Tail();
        #endregion

        #region GOTO_CLICK
        object GotoClick_Head(string selector);
        object GotoClick_Tail();
        #endregion

        #region WHILE_CLICK
        object WhileClick_Head();
        object WhileClick_IterationBegin(string selector);
        object WhileClick_IterationEnd();
        object WhileClick_Tail();
        #endregion
    }
}
