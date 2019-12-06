using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.SeleniumExecutionEngine
{
    public class SeleniumExecutionEngineOptions
    {
        public bool RunHeadlessBrowser { get; set; } = false;
        public bool DisableWebSecurity { get; set; } = false;
    }
}
