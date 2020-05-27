#region TabItemEx 文件信息
/***********************************************************
**文 件 名：TabItemEx 
**命名空间：WpfTest.Skins 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/4/16 16:26:01 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TabControl.UserControls
{
    /// <summary>
    /// TabItemEx
    /// </summary>
    [TemplatePart(Name = "btnClose", Type = typeof(Button))]
    public class TabItemEx : TabItem
    {
        /// <summary>
        /// 父级TabControl
        /// </summary>
        private System.Windows.Controls.TabControl m_Parent;
        /// <summary>
        /// 约定的宽度
        /// </summary>
        private double m_ConventionWidth = 100;

        public TabItemEx()
        {
            DefaultStyleKey = typeof(TabItemEx);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsClosed
        {
            get => (bool)GetValue(IsClosedProperty);
            set => SetValue(IsClosedProperty, value);
        }

        /// <summary>
        /// 是否可关闭
        /// </summary>
        public static readonly DependencyProperty IsClosedProperty =
            DependencyProperty.Register("IsClosed", typeof(bool), typeof(TabItemEx), new PropertyMetadata(true));

        /// <summary>
        /// 关闭时触发命令
        /// </summary>
        public ICommand Close
        {
            get => (ICommand)GetValue(CloseProperty);
            set => SetValue(CloseProperty, value);
        }

        /// <summary>
        /// 关闭时触发命令
        /// </summary>
        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.Register("Close",
                typeof(ICommand),
                typeof(TabItemEx));

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var btnClose = GetTemplateChild("btnClose") as Button;
            if (btnClose != null)
            {
                btnClose.Click += (sender, e) =>
                {
                    if (IsClosed)
                    {
                        Close?.Execute(this);
                    }
                    else
                    {
                        btnClose.Visibility = Visibility.Collapsed;
                    }
                };
            }

            //m_Parent = FindParentTabControl(this);
            //if (m_Parent != null)
            //{
            //    //约定的宽度
            //    double.TryParse(m_Parent.Tag.ToString(), out m_ConventionWidth);
            //    //注册父级TabControl尺寸发生变化事件
            //    m_Parent.SizeChanged += m_Parent_SizeChanged;
            //}
        }

        /// <summary>
        /// 递归找父级TabControl
        /// </summary>
        /// <param name="reference">依赖对象</param>
        /// <returns>TabControl</returns>
        private System.Windows.Controls.TabControl FindParentTabControl(DependencyObject reference)
        {
            DependencyObject dObj = VisualTreeHelper.GetParent(reference);
            if (dObj == null)
                return null;
            if (dObj.GetType() == typeof(System.Windows.Controls.TabControl))
                return dObj as System.Windows.Controls.TabControl;
            else
                return FindParentTabControl(dObj);
        }
        /// <summary>
        /// 当父控件尺寸发生变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //调整自身大小
            //保持约定宽度item的临界个数
            int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth);
            if (m_Parent.Items.Count <= criticalCount)
            {
                //小于等于临界个数 等于约定宽度
                this.Width = m_ConventionWidth;
            }
            else
            {
                //大于临界个数 等于平均宽度
                double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count;
                this.Width = perWidth;
            }      
        }
    }
}
