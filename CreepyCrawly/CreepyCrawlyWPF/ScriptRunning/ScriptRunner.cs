using CreepyCrawly.ExecutionPlanning;
using CreepyCrawly.ExecutionPlanning.Model;
using CreepyCrawly.LanguageEngine;
using CreepyCrawly.Output;
using CreepyCrawly.SeleniumExecutionEngine;
using CreepyCrawlyWPF.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace CreepyCrawlyWPF.ScriptRunning
{
    public class ScriptRunner : IDisposable
    {
        public RunOptions RunOptions { get; private set; }
        public List<string> ErrorMessages { get; private set; }
        private CrawlLangEngine _CrawlLangEngine;
        public event PropertyChangedEventHandler PropertyChanged;
        private EventHandler<NewOutputAppearedEventArgs> _OutputEventHandler;

        public ScriptRunner(RunOptions runOptions, EventHandler<NewOutputAppearedEventArgs> outputEventHandler)
        {
            RunOptions = runOptions;
            _OutputEventHandler = outputEventHandler;
        }
        public async Task StartScript(string scriptText)
        {
            if (TryRunLangEngine(scriptText))
            {
                try
                {
                    using (SeleniumExecutionEngine executionEngine = new SeleniumExecutionEngine(RunOptions.WebDriverPath, new SeleniumExecutionEngineOptions() { DisableWebSecurity = RunOptions.DisableWebSecurity, RunHeadlessBrowser = RunOptions.NoBrowser }))
                    {
                        executionEngine.StartEngine();
                        ExecutionPlan plan = SeleniumExecutionPlanFactory.GenerateExecutionPlan(_CrawlLangEngine.StartingContext, executionEngine);

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
                        if (executionEngine.IsEngineOk)
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
                }
                catch (Exception e)
                {
                    CreepyCrawly.Output.OutputSingleton.WriteToStringOutputters(string.Format("An error occurred during script execution with message:\n{0}\nSee the following stacktrace:\n{1}", e.Message, e.StackTrace));
                }
            }
        }

        private bool TryRunLangEngine(string scriptText)
        {
            _CrawlLangEngine = new CrawlLangEngine(scriptText);
            ErrorMessages = _CrawlLangEngine.Errors;
            return !_CrawlLangEngine.HasErrors;
        }
        public void Dispose()
        {

        }

    }
}
