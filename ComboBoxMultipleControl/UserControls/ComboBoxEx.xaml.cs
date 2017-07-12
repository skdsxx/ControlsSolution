using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ComboBoxMultipleControl.ViewModel;

namespace ComboBoxMultipleControl.UserControls
{
    /// <summary>
    /// ComboBoxEx.xaml 的交互逻辑
    /// </summary>
    public partial class ComboBoxEx : UserControl
    {
        private string SelectedStr { get; set; }
        public ComboBoxEx()
        {
            InitializeComponent();
        }

        private void ComboItemCheckBoxClick_OnClick(object sender, RoutedEventArgs e)
        {
            var items = cb.ItemsSource;
            if (items != null)
            {
                SelectedStr = string.Empty;
                foreach (Student item in items)
                {
                    if (item.IsChecked == true)
                    {
                        SelectedStr = string.Format("{0}{1};", SelectedStr, item.Name);
                    }
                }
                cb.Tag = SelectedStr;
            }
        }
        #region 对外的依赖属性
        public List<Student> SelectedItems1
        {
            get { return (List<Student>)GetValue(SelectedItems1Property); }
            set { SetValue(SelectedItems1Property, value); }
        }
        public static readonly DependencyProperty SelectedItems1Property =
            DependencyProperty.Register("SelectedItems1", typeof(List<Student>), typeof(ComboBoxEx), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectedChangeCallBack)));
        private static void OnSelectedChangeCallBack(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("Success");
        }
        #endregion
    }
}
