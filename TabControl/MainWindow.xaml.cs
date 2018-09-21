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

namespace TabControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow = this;
        }

        private void ButtonStatic_OnClick(object sender, RoutedEventArgs e)
        {
           new TabControl1().Show();
           Application.Current.MainWindow = this;
        }
        private void ButtonDy_OnClick(object sender, RoutedEventArgs e)
        {
            new TabControl2().Show();
            Application.Current.MainWindow = this;
        }
        private void ButtonDy3_OnClick(object sender, RoutedEventArgs e)
        {
            new TabControl3().Show();
            Application.Current.MainWindow = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new TabControl4().Show();
            Application.Current.MainWindow = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new TabControl5().Show();
            Application.Current.MainWindow = this;
        }
    }
}
