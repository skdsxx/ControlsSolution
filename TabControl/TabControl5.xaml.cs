using CommonLibrary.Annotations;
using CommonLibrary.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TabControl.Pages;
using TabControl.UserControls;

namespace TabControl
{
    /// <summary>
    /// TabControl5.xaml 的交互逻辑
    /// </summary>
    public partial class TabControl5 : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// TabControl数据源
        /// </summary>
        public ObservableCollection<TabItemEx> TabItemWithCloses { get; set; }

        /// <summary>
        /// TabItem关闭按钮事件
        /// </summary>
        public ICommand Closed { get; set; }

        public TabControl5()
        {
            InitializeComponent();
            DataContext = this;//界面数据绑定

            Closed= new RelayCommand<TabItemEx>(TabClose);//命令执行初始化
            InitialTreeMenu();
        }

        /// <summary>
        /// 初始化Tab绑定的数据源
        /// </summary>
        private void InitialTreeMenu()
        {
            TabItemWithCloses = new ObservableCollection<TabItemEx>();
            TabItemWithCloses.Add(new TabItemEx { Header = "1控件测试1", Tag = "1", Content = new Page1(),IsClosed=false});
            TabItemWithCloses.Add(new TabItemEx { Header = "2控件测试2", Tag = "2", Content = new Page2(),IsClosed=false});
            TabItemWithCloses.Add(new TabItemEx { Header = "3控件测试3", Tag = "3", Content = new Page3(),IsClosed=true});
            TabItemWithCloses.Add(new TabItemEx { Header = "4控件测试4", Tag = "4", Content = new Page4(),IsClosed=true});
            TabItemWithCloses.Add(new TabItemEx { Header = "5控件测试5", Tag = "5", Content = new Page5(),IsClosed=true});
            // TabItemWithCloses.Add(node);

            TabItemWithCloses.Add(new TabItemEx { Header = "6控件测试6", Tag = "6", Content = new Page1(),IsClosed=true});
            TabItemWithCloses.Add(new TabItemEx { Header = "7控件测试7", Tag = "7", Content = new Page2(),IsClosed=true});
            TabItemWithCloses.Add(new TabItemEx { Header = "8控件测试8", Tag = "8", Content = new Page3(),IsClosed=true});
            TabItemWithCloses.Add(new TabItemEx { Header = "9控件测试9", Tag = "9", Content = new Page4(),IsClosed=true});

            TabItemWithCloses.Add(new TabItemEx { Header = "0控件测试10", Tag = "10", Content = new Page5(),IsClosed=true});


            var tabNew = new TabItemEx { Header = "主页", Content = new MainPage() };
            TabItemWithCloses.Add(tabNew);
            //((MainWindow)Application.Current.MainWindow).TabMain.SelectedItem = TabContents.FirstOrDefault(u => u.Header == tabNew.Header);

            //ICollectionView cv = CollectionViewSource.GetDefaultView(MainWindow.LbMenu.ItemsSource);
            //cv.GroupDescriptions.Add(new PropertyGroupDescription("Parent"));
        }

        /// <summary>
        /// TabItem关闭事件方法
        /// </summary>
        /// <param name="tabItem"></param>
        private void TabClose(TabItemEx tabItem)
        {
            if(TabItemWithCloses!=null)
            {
                TabItemWithCloses.Remove(tabItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
