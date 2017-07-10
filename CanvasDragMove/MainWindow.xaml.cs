using System.Windows;

namespace CanvasDragMove
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonSet_OnClick(object sender, RoutedEventArgs e)
        {
            new MoveWindow(true).Show();
        }

        private void ButtonQuery_OnClick(object sender, RoutedEventArgs e)
        {
            new MoveWindow(false).Show();
        }

        private void ButtonDySet_OnClick(object sender, RoutedEventArgs e)
        {
            new DyMoveWIndow(true).Show();
        }

        private void ButtonDyQuery_OnClick(object sender, RoutedEventArgs e)
        {
            new DyMoveWIndow(false).Show();
        }
    }
}
