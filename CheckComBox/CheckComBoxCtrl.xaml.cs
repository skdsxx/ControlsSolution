using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace CheckComBox
{
    /// <summary>
    /// CheckComBoxCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class CheckComBoxCtrl : UserControl
    {
        public CheckComBoxCtrl()
        {
            InitializeComponent();
        }

        public bool IsLoaded = false;
        public string MarkString              //提示字符串
        {
            get;
            set;
        }



        private int selectedIndex = -1;
        public int SelectedIndex            //选中项的索引，用于选中一项
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (dtDataTable != null)
                {
                    int count = dtDataTable.Rows.Count;
                    if (value >= count || value < 0)
                    {
                        selectedIndex = -1;
                    }
                    else
                    {
                        selectedIndex = value;

                        dtDataTable.Rows[selectedIndex]["IsChecked"] = true;

                        string selectValue = dtDataTable.Rows[selectedIndex][strSelectPath].ToString();
                        string selectShow = dtDataTable.Rows[selectedIndex][strDisplayPath].ToString();

                        this.selectedList.Add(selectValue);
                        this.selectedShow.Add(selectShow);

                        this.SetEditBoxText();
                    }
                }
                else
                {
                    selectedIndex = -1;
                }
            }
        }

        public ComboBox PopBox
        {
            get
            {
                return cob_Box;
            }
        }
        private DataTable dtDataTable;           //用于绑定下拉框的数据源
        public DataTable DataSource         //用于绑定下拉框的数据源
        {
            get
            {
                return dtDataTable;
            }
            set
            {
                dtDataTable = value;

                if (dtDataTable != null)
                {
                    AddSelectColumn(); //给数据源添加标示是否选中的列
                    cob_Box.ItemsSource = dtDataTable.DefaultView;
                }
                else
                {
                    cob_Box.ItemsSource = null;
                }
            }
        }

        private string strDisplayPath;      //组合框的DisplayMemberPath
        public string DisplayMemberPath     //组合框的DisplayMemberPath
        {
            get
            {
                return strDisplayPath;
            }
            set
            {
                strDisplayPath = value;
            }
        }

        private string strSelectPath;       //组合框的SelectedValuePath
        public string SelectedValuePath     //组合框的SelectedValuePath
        {
            get
            {
                return strSelectPath;
            }
            set
            {
                strSelectPath = value;
            }
        }

        private IList<string> selectedList = new List<string>();  //下拉框选中值的列表
        public IList<string> SelectedValues                     //下拉框选中值的列表
        {
            get
            {
                return selectedList;
            }
        }

        private IList<string> selectedShow = new List<string>();   //下拉框选中显示的列表
        public IList<string> SelectedDisplays                      //下拉框选中显示的列表
        {
            get
            {
                return selectedShow;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
            {
                cob_Box.Width = this.ActualWidth;
                txb_EditBox.Width = this.ActualWidth - 25;

                if (MarkString != null && this.selectedIndex < 0)
                {
                    txb_EditBox.Text = MarkString;
                }
            }

            IsLoaded = true;

        }


        private void ckb_CobItem_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            //绑定显示路径
            Binding objBind = new Binding();
            objBind.Path = new PropertyPath(this.DisplayMemberPath);
            ckb.SetBinding(CheckBox.ContentProperty, objBind);

            //绑定选中字段
            Binding objBinding = new Binding();
            objBinding.Path = new PropertyPath(this.SelectedValuePath);
            ckb.SetBinding(CheckBox.TagProperty, objBinding);
        }

        //复选款点击事件
        private void ckb_CobItem_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            string SelectValue = ckb.Tag.ToString();
            string SelectShow = ckb.Content.ToString();

            if (ckb.IsChecked == true)
            {
                if (!this.selectedList.Contains(SelectValue))
                {
                    this.selectedList.Add(SelectValue);
                    this.selectedShow.Add(SelectShow);
                }
            }
            else if (ckb.IsChecked == false)
            {
                if (this.selectedList.Contains(SelectValue))
                {
                    this.selectedList.Remove(SelectValue);
                    this.selectedShow.Remove(SelectShow);
                }
            }

            this.SetEditBoxText();
        }

        // 设置编辑框中的文本
        private void SetEditBoxText()
        {
            this.txb_EditBox.Clear();
            if (this.selectedShow.Count > 0)
            {
                string strShow = "";
                for (int i = 0; i < this.selectedShow.Count; i++)
                {
                    string showItem = this.selectedShow[i];
                    strShow = strShow + showItem + ",";
                }
                this.txb_EditBox.Text = strShow.Substring(0, strShow.Length - 1);
            }
            else
            {
                if (MarkString != null)
                {
                    this.txb_EditBox.Text = MarkString;
                }
            }
        }

        /// <summary>
        /// 给数据源添加标示是否选中的列
        /// </summary>
        private void AddSelectColumn()
        {
            if (dtDataTable != null)
            {
                dtDataTable.Columns.Add("IsChecked", typeof(bool));

                for (int i = 0; i < dtDataTable.Rows.Count; i++)
                {
                    dtDataTable.Rows[i]["IsChecked"] = false;
                }
            }
        }
    }
}
