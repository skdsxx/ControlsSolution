using System.Windows;
using TabControl.ViewModels;

namespace TabControl
{
    /// <summary>
    /// TabControl2.xaml 的交互逻辑
    /// </summary>
    public partial class TabControl2 : Window
    {
        public TabControl2()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            DataContext=new TabControl2ViewModel();
        }
    }
}
