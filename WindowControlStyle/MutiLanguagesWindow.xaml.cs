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
using System.Windows.Shapes;

namespace WindowControlStyle
{
    /// <summary>
    /// MutiLanguagesWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MutiLanguagesWindow : Window
    {
        private string _currentLang = string.Empty;
        public MutiLanguagesWindow()
        {
            InitializeComponent();

            _currentLang = "Zh";
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            XmlDataProvider provider = TryFindResource("Lang") as XmlDataProvider;

            if (provider == null)
                return;

            if (_currentLang == "Zh")
            {
                provider.Source = new Uri("Languages/English.xml", UriKind.Relative);

                _currentLang = "En";
            }
            else
            {
                provider.Source = new Uri("Languages/Chinese.xml", UriKind.Relative);

                _currentLang = "Zh";
            }

            provider.Refresh();
        }
    }
}
