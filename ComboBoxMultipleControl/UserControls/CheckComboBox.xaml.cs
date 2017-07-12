using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComboBoxMultipleControl.UserControls
{
    /// <summary>
    /// CheckComboBox.xaml 的交互逻辑
    /// </summary>
    public partial class CheckComboBox : UserControl
    {

        #region 下拉框属性

        public bool IsLoaded;
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

                        selectedList.Add(selectValue);
                        selectedShow.Add(selectShow);

                        SetEditBoxText();
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
                return cb;
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
                    cb.ItemsSource = dtDataTable.DefaultView;
                }
                else
                {
                    cb.ItemsSource = null;
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

        private string _selectStr;       //组合框的选择显示的文本
        public string SelectStr         //组合框的选择显示的文本
        {
            get
            {
                return _selectStr;
            }
            set
            {
                _selectStr = value;
                cb.Text = _selectStr;
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
        #endregion

        public CheckComboBox()
        {
            InitializeComponent();
        }

        private void CheckComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
            {
                if (MarkString != null && selectedIndex < 0)
                {
                    cb.Text = MarkString;
                }
            }

            IsLoaded = true;
        }
        // 设置编辑框中的文本
        private void SetEditBoxText()
        {
            cb.Text = string.Empty;
            if (selectedShow.Count > 0)
            {
                string strShow = "";
                for (int i = 0; i < selectedShow.Count; i++)
                {
                    string showItem = selectedShow[i];
                    strShow = strShow + showItem + ",";
                }
                cb.Text = strShow.Substring(0, strShow.Length - 1);
            }
            else
            {
                if (MarkString != null)
                {
                    cb.Text = MarkString;
                }
            }
            SelectStr = cb.Text;
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

        private void Ckb_CobItem_OnClick(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            if (ckb == null) return;
            string SelectValue = ckb.Tag.ToString();
            string SelectShow = ckb.Content.ToString();

            if (string.IsNullOrEmpty(SelectStr) || SelectStr == MarkString)
            {
                selectedList.Clear();
                selectedShow.Clear();
            }

            if (ckb.IsChecked == true)
            {
                if (!selectedList.Contains(SelectValue))
                {
                    selectedList.Add(SelectValue);
                    selectedShow.Add(SelectShow);
                }
            }
            else if (ckb.IsChecked == false)
            {
                if (selectedList.Contains(SelectValue))
                {
                    selectedList.Remove(SelectValue);
                    selectedShow.Remove(SelectShow);
                }
            }

            SetEditBoxText();
        }

        private void Ckb_CobItem_OnLoaded(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            //绑定显示路径
            Binding objBind = new Binding();
            objBind.Path = new PropertyPath(DisplayMemberPath);
            ckb.SetBinding(ContentProperty, objBind);

            //绑定选中字段
            Binding objBinding = new Binding();
            objBinding.Path = new PropertyPath(SelectedValuePath);
            ckb.SetBinding(TagProperty, objBinding);
        }

       
    }
}
