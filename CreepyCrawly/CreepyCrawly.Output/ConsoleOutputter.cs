using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.Output
{
    class ConsoleOutputter : ITextOutputter
    {
        public ConsoleOutputter()
        {

        }

        public void WriteOutput(object output)
        {
            if(output != null)
            {
                Console.WriteLine(output.ToString());
            }                
        }
    }
}
