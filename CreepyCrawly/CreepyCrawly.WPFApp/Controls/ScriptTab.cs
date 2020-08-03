using CreepyCrawly.WPFApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace CreepyCrawly.WPFApp.Controls
{
    public class ScriptTab : CloseableTab
    {
        public ScriptPage ScriptPage { get; set; }
        private Action<ScriptTab> _Close;
        public ScriptTab(string title, ScriptPage scriptPage, Action<ScriptTab> close) : base(title)
        {
            var frame = new Frame();
            frame.Content = scriptPage;
            Content = frame;
           
            ScriptPage = scriptPage;
            _Close = close;
        }
        public override void btn_close_Click(object sender, MouseEventArgs e)
        {
            _Close.Invoke(this);
        }
    }
}
