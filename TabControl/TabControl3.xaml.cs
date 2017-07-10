using System.Windows;
using TabControl.ViewModels;

namespace TabControl
{
    /// <summary>
    /// TabControl3.xaml 的交互逻辑
    /// </summary>
    public partial class TabControl3 : Window
    {
        public TabControl3()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            DataContext =new TabControl3ViewModel();
        }
    }
}
