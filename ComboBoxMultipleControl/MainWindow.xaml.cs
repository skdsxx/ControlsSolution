using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ComboBoxMultipleControl.Annotations;

namespace ComboBoxMultipleControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Box.DataSource = this.CreateDataSource();
            Box.DisplayMemberPath = "值";
            Box.SelectedValuePath = "序号";
            Box.PopBox.DropDownClosed += new EventHandler(PopBox_DropDownClosed);
        }

        void PopBox_DropDownClosed(object sender, EventArgs e)
        {
            string SelectedContent = "";
            foreach (string s in this.Box.SelectedValues)
            {
                SelectedContent += (s + "  ");
            }
            MessageBox.Show(SelectedContent);
        }

        /// <summary>
        /// 创建DataSource
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("值", typeof(string));

            DataRow dr1 = dt.NewRow();
            dr1["序号"] = 1;
            dr1["值"] = "北京";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["序号"] = 2;
            dr2["值"] = "上海";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["序号"] = 3;
            dr3["值"] = "天津";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["序号"] = 4;
            dr4["值"] = "香港";
            dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr5["序号"] = 5;
            dr5["值"] = "河北";
            dt.Rows.Add(dr5);

            return dt;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
