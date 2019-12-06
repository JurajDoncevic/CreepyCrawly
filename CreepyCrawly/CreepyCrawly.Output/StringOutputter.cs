using System;
using System.Collections.Generic;
using System.Text;

namespace CreepyCrawly.Output
{
    class StringOutputter : ITextOutputter
    {
        public event EventHandler<NewOutputAppearedEventArgs> NewOutputAppeared;

        public StringOutputter()
        {

        }

        public void WriteOutput(object output)
        {
            OnThresholdReached(new NewOutputAppearedEventArgs(output.ToString()));
        }

        protected virtual void OnThresholdReached(NewOutputAppearedEventArgs e)
        {
            NewOutputAppeared?.Invoke(this, e);
        }
    }

    public class NewOutputAppearedEventArgs : EventArgs
    {
        public string Output { get; set; }
        public DateTime TimeAppeared { get; private set; } = DateTime.Now;
        public NewOutputAppearedEventArgs(string output)
        {
            Output = output;
        }
    }
}
