using CommonLibrary.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TabControl.Models;
using TabControl.Pages;
using TabControl.UserControls;

namespace TabControl
{
    /// <summary>
    /// TabControl4.xaml 的交互逻辑
    /// </summary>
    public partial class TabControl4 : Window
    {
        //这种方式适用于静态绑定的tab页
        public TabControl4()
        {
            InitializeComponent();
            DataContext = this;
        }       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabItemWithClose item = new TabItemWithClose();
            item.Header = string.Format("Header{0}", tab_Main.Items.Count);
            item.ToolTip = string.Format("Header{0}", tab_Main.Items.Count);
            item.Margin = new Thickness(0, 0, 1, 0);
            item.Height = 28;
            Label lbl = new Label() { Content = string.Format("Label{0}", tab_Main.Items.Count) };
            Button btn = new Button() { Width = 132, Height = 32, Content = string.Format("Button{0}", tab_Main.Items.Count) };
            StackPanel sPanel = new StackPanel();
            sPanel.Children.Add(lbl);
            sPanel.Children.Add(btn);
            item.Content = sPanel;
            tab_Main.Items.Add(item);
        }
    }
    
}
