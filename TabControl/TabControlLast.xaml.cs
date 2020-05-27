//=====================================================
//CLR版本: 4.0.30319.42000
//新建项输入的名称: TabControlLast
//命名空间名称: TabControl
//文件名: TabControlLast
//当前系统时间: 2020/5/27 14:06:48
//创建年份: 2020
//======================================================
//Copyright：  @2020@   All rights are reserved
//======================================================
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TabControl.Pages;
using TabControl.UserControls;

namespace TabControl
{
    /// <summary>
    /// TabControlLast.xaml 的交互逻辑
    /// </summary>
    public partial class TabControlLast : Window
    {
        /// <summary>
        /// TabControl数据源
        /// </summary>
        public ObservableCollection<TabItemBox> Contents { get; set; }
        public TabControlLast()
        {
            InitializeComponent();
            DataContext = this;

            Contents = new ObservableCollection<TabItemBox>
            {
                new TabItemBox{Header="主页",CanClosed=false,Content=new MainPage()},
                new TabItemBox{Header="页面1",CanClosed=false,Content=new Page1()},
                new TabItemBox{Header="页面2",CanClosed=true,Content=new Page2()},
                new TabItemBox{Header="页面3",CanClosed=true,Content=new Page3()},
                new TabItemBox{Header="页面4",CanClosed=true,Content=new Page4()},

            };
        }

        private int i = 0;
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Contents.Add(new TabItemBox
            {
                Header = "测试" + i,
                Content = "测试" + i,
                CanClosed=true
            });
            i++;
        }
    }
}
