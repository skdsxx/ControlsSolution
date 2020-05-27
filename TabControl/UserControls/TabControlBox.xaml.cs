//=====================================================
//CLR版本: 4.0.30319.42000
//新建项输入的名称: TabControlBox
//命名空间名称: TabControl.UserControls
//文件名: TabControlBox
//当前系统时间: 2020/5/27 13:58:53
//创建年份: 2020
//======================================================
//Copyright：  @2020@   All rights are reserved
//======================================================
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace TabControl.UserControls
{
    /// <summary>
    /// TabControlBox.xaml 的交互逻辑
    /// </summary>
    public partial class TabControlBox : System.Windows.Controls.TabControl
    {
        /// <summary>
        /// TabItem右键菜单源
        /// </summary>
        private TabItemBox _contextMenuSource;
        public TabControlBox()
        {
            InitializeComponent();
        }
        #region 依赖属性

        public ObservableCollection<TabItemBox> Documents
        {
            get => (ObservableCollection<TabItemBox>)GetValue(DocumentsProperty);
            set => SetValue(DocumentsProperty, value);
        }

        public static readonly DependencyProperty DocumentsProperty = DependencyProperty.Register("Documents",
            typeof(ObservableCollection<TabItemBox>), typeof(TabControlBox), new PropertyMetadata(OnDocumentsChanged));

        /// <summary>
        /// 集合绑定变化时回调函数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDocumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tab = d as TabControlBox;

            if (tab != null)
            {
                var newSource = e.NewValue as ObservableCollection<TabItemBox>;
                tab.ItemsSource = newSource;
            }
        }
        #endregion

        /// <summary>
        /// TabItem右键按下事件--菜单绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _contextMenuSource = (sender as Grid).TemplatedParent as TabItemBox;
            menu.PlacementTarget = sender as Grid;
            menu.Placement = PlacementMode.MousePoint;
            menu.IsOpen = true;
        }
        /// <summary>
        /// TabItem右键菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem btn = e.Source as MenuItem;
            int data = Convert.ToInt32(btn.CommandParameter.ToString());

            if (_contextMenuSource != null)
            {
                List<TabItemBox> tabItemList = new List<TabItemBox>();
                if (data == 0)
                {
                    tabItemList.Add(_contextMenuSource);
                }
                if (data == 1)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        TabItemBox tabItem = Items[i] as TabItemBox;
                        if (tabItem != _contextMenuSource)
                        {
                            tabItemList.Add(tabItem);
                        }
                    }
                }
                if (data == 2)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        TabItemBox tabItem = Items[i] as TabItemBox;
                        if (tabItem != _contextMenuSource)
                        {
                            tabItemList.Add(tabItem);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (data == 3)
                {
                    for (int i = Items.Count - 1; i >= 0; i--)
                    {
                        TabItemBox tabItem = Items[i] as TabItemBox;
                        if (tabItem != _contextMenuSource)
                        {
                            tabItemList.Add(tabItem);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                foreach (TabItemBox tabItem in tabItemList)
                {
                    CloseTabItem(tabItem);
                }
            }
        }

        /// <summary>
        /// 关闭TabItem
        /// </summary>
        private void CloseTabItem(TabItemBox tabItem)
        {
            if (tabItem!=null&&tabItem.CanClosed)
            {
                Documents?.Remove(tabItem);
            }
        }

        /// <summary>
        /// TabControl 选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (TabItem tabItem in e.RemovedItems)
            {
                Panel.SetZIndex(tabItem, 99);
            }
            foreach (TabItem tabItem in e.AddedItems)
            {
                Panel.SetZIndex(tabItem, 999);
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClosed_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var header = btn.Tag.ToString();
                var tab = Documents.FirstOrDefault(p => p.Header.ToString() == header);
                if (tab != null)
                {
                    Documents?.Remove(tab);
                }
            }
        }
    }

    /// <summary>
    /// 自定义TabItem项，增加可是否可关闭按钮
    /// </summary>
    public class TabItemBox : TabItem
    {
        public TabItemBox()
        {
            //设置默认样式识别的名称
            DefaultStyleKey = typeof(TabItemBox);
        }

        /// <summary>
        /// 是否可关闭属性
        /// </summary>
        public bool CanClosed
        {
            get => (bool)GetValue(CanClosedProperty);
            set => SetValue(CanClosedProperty, value);
        }

        /// <summary>
        /// 是否可关闭
        /// </summary>
        public static readonly DependencyProperty CanClosedProperty =
            DependencyProperty.Register("CanClosed", typeof(bool), typeof(TabItemEx), new PropertyMetadata(true));

        /// <summary>
        /// 模板继承
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
