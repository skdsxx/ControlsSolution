#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: DESKTOP-L71O6PR
  * 命名空间: WindowControlStyle.Skins
  * 文 件 名: ControlExtension
  * 创建人员: SunXian
  * 创建时间: 2018/11/30 18:12:54

//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion

using System;
using System.Windows;
using System.Windows.Input;

namespace WindowControlStyle.Skins
{
    public static class ControlExtension
    {
        #region BindCommand

        /// <summary>
        /// 绑定命令和命令事件到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement @ui, ICommand com, Action<object, ExecutedRoutedEventArgs> call)
        {
            var bind = new CommandBinding(com);
            bind.Executed += new ExecutedRoutedEventHandler(call);
            @ui.CommandBindings.Add(bind);
        }

        /// <summary>
        /// 绑定RelayCommand命令到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement @ui, RoutedUICommand close, RelayCommand<object> com)
        {
            var bind = new CommandBinding(com);
            bind.Executed += delegate (object sender, ExecutedRoutedEventArgs e)
            {
                com.Execute(e.Parameter);
            };
            @ui.CommandBindings.Add(bind);
        }

        #endregion
    }
}
