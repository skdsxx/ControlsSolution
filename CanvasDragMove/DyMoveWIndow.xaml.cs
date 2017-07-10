using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CanvasDragMove.InfoConfigService;

namespace CanvasDragMove
{
    /// <summary>
    /// DyMoveWIndow.xaml 的交互逻辑
    /// </summary>
    public partial class DyMoveWIndow : Window
    {
        //public List<WindowApplicationSettings> Settings= new List<WindowApplicationSettings>();
        WindowApplicationSettings setting=new WindowApplicationSettings();
        public bool IsAllowDrag { get; set; }
        public DyMoveWIndow(bool isDrag)
        {
            InitializeComponent();
            IsAllowDrag = isDrag;

            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

        }

        private void InitAddButton()
        {
            setting.Models=new List<BtnModel>();
            using (var client = new InfoConfigClient())
            {
                var cameras = client.GetAllBsCamera();
                if (cameras == null || cameras.Length == 0) return;
                foreach (var dto in cameras)
                {
                    //var btn=new Button();
                    //btn.Name = dto.Name;
                    //btn.Content = dto.ChNo;
                    //LayoutRoot.Children.Add(btn);
                    //LayoutRoot.RegisterName(btn.Name, btn);
                    //var str = btn.Content==null ? "" : btn.Content.ToString();
                    //var qq = new BtnModel
                    //{
                    //    WinName = btn.Name,
                    //    WinCode =str ,
                    //    ButtonLocation = new Point(0.1, 0.1)
                    //};
                    var str =string.IsNullOrEmpty(dto.ChNo) ? "" :dto.ChNo;
                    var qq = new BtnModel
                    {
                        WinName = dto.Name,
                        WinCode = str,
                        ButtonLocation = new Point(0.1, 0.1)
                    };
                    setting.Models.Add(qq);
                    //setting.WinCode = dto.Name;
                    //setting.WinText = dto.ChNo;                   
                }
                setting.Save();
                MainWindow_Loaded(null, null);
            }
           
        }

        #region 窗口打开时，控件位置初始化;关闭时，保存控件位置
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (setting.Models != null && setting.Models.Count > 0)
            {
                foreach (var model in setting.Models)
                {
                    Button btn = LayoutRoot.FindName(model.WinName) as Button;//找到刚新添加的按钮 
                    if (btn != null)
                    {
                        var x = double.IsNaN((double) btn.GetValue(Canvas.LeftProperty))?0.1: (double)btn.GetValue(Canvas.LeftProperty);
                        var y = double.IsNaN((double)btn.GetValue(Canvas.TopProperty)) ? 0.1 : (double)btn.GetValue(Canvas.TopProperty);
                        model.ButtonLocation = new Point(x,y);
                    }
                }
            }
                   
            setting.Save();
            //settings.ButtonLocation = new Point((double)button1.GetValue(Canvas.LeftProperty),
            //    (double)button1.GetValue(Canvas.TopProperty));
            //settings.TextBoxLocation = new Point((double)tb.GetValue(Canvas.LeftProperty),
            //    (double)tb.GetValue(Canvas.TopProperty));
            //settings.WinText = tb.Text;
            //settings.LabelLocation = new Point((double)label1.GetValue(Canvas.LeftProperty),
            //    (double)label1.GetValue(Canvas.TopProperty));
            //settings.Save();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            setting.Reload();
            if (setting.Models != null && setting.Models.Count > 0)
            {
                foreach (var model in setting.Models)
                {
                    var btn = new Button { Name = model.WinName, Content = model.WinCode};
                    LayoutRoot.Children.Add(btn);
                    LayoutRoot.RegisterName(btn.Name, btn);
                    btn.SetValue(Canvas.LeftProperty, model.ButtonLocation.X);
                    btn.SetValue(Canvas.TopProperty, model.ButtonLocation.Y);
                }
            }
          
            foreach (UIElement uiEle in LayoutRoot.Children)
            {
                //WPF设计上的问题,Button.Clicked事件Supress掉了Mouse.MouseLeftButtonDown附加事件等.
                //不加这个Button、TextBox等无法拖动
                if (uiEle is Button || uiEle is TextBox)
                {
                    uiEle.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(Element_MouseLeftButtonDown), true);
                    uiEle.AddHandler(MouseMoveEvent, new MouseEventHandler(Element_MouseMove), true);
                    uiEle.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(Element_MouseLeftButtonUp), true);
                    continue;
                }

                uiEle.MouseMove += Element_MouseMove;
                uiEle.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                uiEle.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            }
        }
        #endregion

        #region 控件拖拽

        bool isDragDropInEffect;
        Point pos;

        void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragDropInEffect && IsAllowDrag)
            {
                FrameworkElement currEle = sender as FrameworkElement;
                double xPos = e.GetPosition(null).X - pos.X + (double)currEle.GetValue(Canvas.LeftProperty);
                double yPos = e.GetPosition(null).Y - pos.Y + (double)currEle.GetValue(Canvas.TopProperty);
                currEle.SetValue(Canvas.LeftProperty, xPos);
                currEle.SetValue(Canvas.TopProperty, yPos);
                pos = e.GetPosition(null);
            }
        }

        void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            FrameworkElement fEle = sender as FrameworkElement;
            isDragDropInEffect = true;
            pos = e.GetPosition(null);
            fEle.CaptureMouse();
            fEle.Cursor = Cursors.Hand;
        }

        void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement ele = sender as FrameworkElement;
                isDragDropInEffect = false;
                ele.ReleaseMouseCapture();
            }
        }
        #endregion

        private void ButtonInitAdd_OnClick(object sender, RoutedEventArgs e)
        {
            InitAddButton();
        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            if (setting.Models != null && setting.Models.Count > 0)
            {
                setting.Models.Clear();
                setting.Save();
                LayoutRoot.Children.Clear();
            }
            MainWindow_Loaded(sender, e);
        }
    }
}
