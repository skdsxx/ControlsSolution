using System.Windows;
using System.Windows.Controls;
using WindowControlStyle.Resources;

namespace WindowControlStyle
{
    /// <summary>
    /// MutiLanguageWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class MutiLanguageWindow2 : Window
    {
        public MutiLanguageWindow2()
        {
            InitializeComponent();
            LanguageInit.Initialize();
            LanguageInit.CurrentLanguage = "zh";
            this.comboBox1.ItemsSource = LanguageInit.LanguageIndex;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null || e.AddedItems[0] == null)
                return;
            Language selectLanguage = e.AddedItems[0] as Language;
            LanguageInit.CurrentLanguage = selectLanguage.LanguageCode;
        }
    }
}
