#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: SUNXIAN
  * 命名空间: TabControl.ViewModels
  * 文 件 名: TabControl2ViewModel
  * 创建人员: SunXian
  * 创建时间: 2017/5/3 16:31:26
  
//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommonLibrary.Command;
using TabControl.Models;
using TabControl.Pages;

namespace TabControl.ViewModels
{
    public class TabControl2ViewModel
    {
        public ObservableCollection<TabItem> TabContents { get; set; }
        public MainWindowModel Model { get; set; }

        public ObservableCollection<TreeViewContent> TreeViewContents { get; set; }
        /// <summary>
        /// 定义执行MainWindow中TreeView的点击事件
        /// </summary>
        public ICommand ShowPageCommand { get; }
        /// <summary>
        /// 主页左侧缩进命令
        /// </summary>
        public ICommand SpliterExpandCommand { get; }

        /// <summary>
        /// Tab页关闭按钮事件
        /// </summary>
        public ICommand TabCloseCommand { get; set; }
        //菜单项缩进的标志
        private bool _isLeftHide;
        public TabControl2ViewModel()
        {
            TabContents = new ObservableCollection<TabItem>();
            Model=new MainWindowModel();
            //初始化执行TreeView的点击事件，AnotherCommandImplementation为项目中实现ICommand接口的事件执行辅助类
            ShowPageCommand = new RelayCommand<TreeViewContent>(ShowPage);
            SpliterExpandCommand = new RelayCommand(SpliterExpand);
            TabCloseCommand=new RelayCommand<TabItem>(TabClose);
            InitialTreeMenu();
        }

        /// <summary>
        /// 点击主菜单显示相应页面
        /// </summary>
        /// <param name="treeItem"></param>
        public void ShowPage(TreeViewContent treeItem)
        {
            if (treeItem != null)
            {
                var tabItem = TabContents.FirstOrDefault(u => u.Header.ToString() == treeItem.Header);
                if (tabItem != null)
                {
                    tabItem.Tag = "0";
                    tabItem.IsSelected = true;
                    Model.SelectedTabItem = tabItem;
                }
                else if (treeItem.Flag == null)
                {
                    var tabNew = new TabItem { Header = treeItem.Header, Tag = "1", Content = "未完成", IsSelected = true };
                    TabContents.Add(tabNew);
                    Model.SelectedTabItem = TabContents.FirstOrDefault(u => u.Header == tabNew.Header);
                    //((TabControl2)Application.Current.MainWindow).TabControl.
                }
                else
                {
                    var tabNew = new TabItem { Header = treeItem.Header, Tag = "1", Content = treeItem.Flag, IsSelected = true };
                    TabContents.Add(tabNew);
                    Model.SelectedTabItem = TabContents.FirstOrDefault(u => u.Header == tabNew.Header);
                }
            }
           
        }

        private void InitialTreeMenu()
        {
            TreeViewContents = new ObservableCollection<TreeViewContent>();
            TreeViewContents.Add(new TreeViewContent { Header = "控件测试", Icon = "../Images/atm.png", Parent = "系统设置", Flag = new Page4() });
            TreeViewContents.Add(new TreeViewContent { Header = "DataGridTest", Icon = "../Images/blackerror.png", Parent = "系统设置", Flag = new Page1() });
            TreeViewContents.Add(new TreeViewContent { Header = "ShowGifPicture", Icon = "/Images/atm.png", Parent = "系统设置", Flag = new Page2() });
            TreeViewContents.Add(new TreeViewContent { Header = "角色管理", Icon = "/Images/grayerror.png", Parent = "系统设置", Flag = new Page3() });
            TreeViewContents.Add(new TreeViewContent { Header = "基础设置", Icon = "/Images/atm.png", Parent = "系统设置", Flag = new Page5() });
            // TreeViewContents.Add(node);

            TreeViewContents.Add(new TreeViewContent { Header = "日志审计", Icon = "/Images/atm.png", Parent = "广播播放" });
            TreeViewContents.Add(new TreeViewContent { Header = "操作日志", Icon = "/Images/atm.png", Parent = "广播播放", Flag = new Page3() });
            TreeViewContents.Add(new TreeViewContent { Header = "异常记录", Icon = "/Images/atm.png", Parent = "广播播放", Flag = new Page4() });
            TreeViewContents.Add(new TreeViewContent { Header = "播放记录", Flag = new Page2(), Parent = "广播播放" });

            TreeViewContents.Add(new TreeViewContent { Header = "用户管理", Parent = "门户设置" });
            TreeViewContents.Add(new TreeViewContent { Header = "登记验证", Parent = "门户设置", Flag = new Page3() });
            TreeViewContents.Add(new TreeViewContent { Header = "黑名单管理", Parent = "门户设置", Flag = new Page4() });
            TreeViewContents.Add(new TreeViewContent { Header = "安检验证", Flag = new Page1(), Parent = "门户设置" });


            var tabNew = new TabItem {Header = "主页",Content = new MainPage()};
            TabContents.Add(tabNew);
            //((MainWindow)Application.Current.MainWindow).TabMain.SelectedItem = TabContents.FirstOrDefault(u => u.Header == tabNew.Header);

            //ICollectionView cv = CollectionViewSource.GetDefaultView(MainWindow.LbMenu.ItemsSource);
            //cv.GroupDescriptions.Add(new PropertyGroupDescription("Parent"));
        }

        /// <summary>
        /// 主页左侧菜单缩进方法函数
        /// </summary>
        public void SpliterExpand()
        {
            //Left Bar hide and show
            _isLeftHide = !_isLeftHide;
            if (_isLeftHide)
            {
                ((TabControl2)Application.Current.MainWindow).GridForm.ColumnDefinitions[0].Width = new GridLength(1d);
            }
            else
            {
                ((TabControl2)Application.Current.MainWindow).GridForm.ColumnDefinitions[0].Width = new GridLength(200d);
            }
        }

        public void TabClose(TabItem item)
        {
            var tab=TabContents.FirstOrDefault(p => p.Header == item.Header);
            if (tab != null)
            {
                TabContents.Remove(item);
            }
        }
    }
}
