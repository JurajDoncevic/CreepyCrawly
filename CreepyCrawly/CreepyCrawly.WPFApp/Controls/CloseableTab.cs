using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CreepyCrawly.WPFApp.Controls
{
    public class CloseableTab : TabItem
    {
        public Page Page { get; set; }
        public string Title
        {
            set
            {
                ((CloseableHeader)this.Header).lbl_tabTitle.Content = value;
            }
        }
        public CloseableTab(string title)
        {
            CloseableHeader closableTabHeader = new CloseableHeader();
            // Assign the usercontrol to the tab header
            this.Header = closableTabHeader;

            closableTabHeader.btn_close.MouseDown +=
               new MouseButtonEventHandler(btn_close_Click);
            closableTabHeader.lbl_tabTitle.SizeChanged +=
               new SizeChangedEventHandler(lbl_TabTitle_SizeChanged);
            Title = title;
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            ((CloseableHeader)this.Header).btn_close.Visibility = Visibility.Visible;
        }
        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            ((CloseableHeader)this.Header).btn_close.Visibility = Visibility.Hidden;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            ((CloseableHeader)this.Header).btn_close.Visibility = Visibility.Visible;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.IsSelected)
            {
                ((CloseableHeader)this.Header).btn_close.Visibility = Visibility.Hidden;
            }
        }

        // Button Close Click - Remove the Tab - (or raise
        // an event indicating a "CloseTab" event has occurred)
        public virtual void btn_close_Click(object sender, MouseEventArgs e)
        {
            ((TabControl)this.Parent).Items.Remove(this);
        }
        // Label SizeChanged - When the Size of the Label changes
        // (due to setting the Title) set position of button properly
        void lbl_TabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((CloseableHeader)this.Header).btn_close.Margin = new Thickness(
               ((CloseableHeader)this.Header).lbl_tabTitle.ActualWidth + 5, 3, 4, 0);
        }

    }
}
