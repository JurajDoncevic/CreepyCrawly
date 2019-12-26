using CreepyCrawly.LanguageEngine;
using CreepyCrawly.LanguageEngine.CommandModel;
using CreepyCrawly.Output;
using CreepyCrawly.SeleniumExecution;
using CreepyCrawly.WPFApp.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace CreepyCrawly.WPFApp.ScriptRunning
{
    public class ScriptRunner : IDisposable
    {
        public RunOptions RunOptions { get; private set; }
        public List<string> ErrorMessages { get; private set; }
        private CrawlLangEngine _CrawlLangEngine;
        public event PropertyChangedEventHandler PropertyChanged;
        private EventHandler<NewOutputAppearedEventArgs> _OutputEventHandler;
        private SeleniumExecution.SeleniumExecutionEngine _SeleniumExecutionEngine;
        private bool IsRunning = false;

        public ScriptRunner(RunOptions runOptions, EventHandler<NewOutputAppearedEventArgs> outputEventHandler)
        {
            RunOptions = runOptions;
            _OutputEventHandler = outputEventHandler;
            ErrorMessages = new List<string>();
        }
        public void StartScript(string scriptText)
        {
            using (_SeleniumExecutionEngine = new SeleniumExecution.SeleniumExecutionEngine(RunOptions.WebDriverPath, new SeleniumExecutionEngineOptions() { DisableWebSecurity = RunOptions.DisableWebSecurity, RunHeadlessBrowser = RunOptions.NoBrowser }))
            {
                _CrawlLangEngine = new CrawlLangEngine(scriptText, _SeleniumExecutionEngine);
                _CrawlLangEngine.ParseScript();
                if (_CrawlLangEngine.HasErrors)
                {
                    ErrorMessages = _CrawlLangEngine.Errors;
                }
                else
                {
                    ExecutionPlan plan = _CrawlLangEngine.GenerateExecutionPlan();
                    try
                    {
                        _SeleniumExecutionEngine.StartEngine();
                        
                        IsRunning = true;
                        OutputSingleton.ClearAllOutputters();
                        if (!string.IsNullOrWhiteSpace(RunOptions.OutputFilePath))
                        {
                            CreepyCrawly.Output.OutputSingleton.CreateFileTextOutputter(RunOptions.OutputFilePath);
                        }
                        if (!string.IsNullOrWhiteSpace(RunOptions.ImageOutputDirectory))
                        {
                            CreepyCrawly.Output.OutputSingleton.CreateImageFileOutputter(RunOptions.ImageOutputDirectory);
                        }
                        CreepyCrawly.Output.OutputSingleton.CreateStringOutputter();
                        CreepyCrawly.Output.OutputSingleton.AssignEventHandlerToStringOutputters(_OutputEventHandler);
                        if (_SeleniumExecutionEngine.IsEngineOk)
                        {
                            plan.Commands.ForEach(cmd =>
                            {
                                cmd.Execute();
                            });
                        }
                        else
                        {
                            CreepyCrawly.Output.OutputSingleton.WriteToStringOutputters("Engine wasn't started :(\nMaybe you are missing an appropriate chromedriver in the app root directory.");
                        }
                    }
                    catch (Exception e)
                    {
                        if (IsRunning)
                            CreepyCrawly.Output.OutputSingleton.WriteToStringOutputters(string.Format("An error occurred during script execution with message:\n{0}\nSee the following stacktrace:\n{1}", e.Message, e.StackTrace));
                    }
                }
            }
        }

        public void StopScript()
        {
            if (_SeleniumExecutionEngine != null)
            {
                IsRunning = false;
                _SeleniumExecutionEngine.StopEngine();
            }
        }

        public void Dispose()
        {

        }

    }
}
