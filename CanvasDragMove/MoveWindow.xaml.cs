using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CanvasDragMove
{
    /// <summary>
    /// MoveWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MoveWindow : Window
    {
        WindowApplicationSettings settings = new WindowApplicationSettings();
        public bool IsAllowDrag { get; set; }
        public MoveWindow(bool isDrag)
        {
            InitializeComponent();

            IsAllowDrag = isDrag;

            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

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
        #region 窗口打开时，控件位置初始化;关闭时，保存控件位置
        void MainWindow_Closing(object sender, CancelEventArgs e)
         {
            //settings.ButtonLocation = new Point((double)button1.GetValue(Canvas.LeftProperty),
            //    (double)button1.GetValue(Canvas.TopProperty));
            //settings.TextBoxLocation=new Point((double)tb.GetValue(Canvas.LeftProperty),
            //    (double)tb.GetValue(Canvas.TopProperty));
            //settings.WinText = tb.Text;
            //settings.LabelLocation= new Point((double)label1.GetValue(Canvas.LeftProperty),
            //    (double)label1.GetValue(Canvas.TopProperty));
            //settings.Save();
         }
 
         void MainWindow_Loaded(object sender, RoutedEventArgs e)
         {
             //settings.Reload();
             //tb.Text = settings.WinText;
             ////button1.Margin= new Thickness(settings.ButtonLocation.X, settings.ButtonLocation.Y,0,0);
            
             //button1.SetValue(Canvas.LeftProperty, settings.ButtonLocation.X);
             //button1.SetValue(Canvas.TopProperty, settings.ButtonLocation.Y);
             //label1.SetValue(Canvas.LeftProperty, settings.LabelLocation.X);
             //label1.SetValue(Canvas.TopProperty, settings.LabelLocation.Y);
             //tb.SetValue(Canvas.LeftProperty, settings.TextBoxLocation.X);
             //tb.SetValue(Canvas.TopProperty, settings.TextBoxLocation.Y);

        }
    #endregion

    #region 控件拖拽

        bool isDragDropInEffect;
        Point pos;

        void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragDropInEffect&&IsAllowDrag)
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

    }
}
