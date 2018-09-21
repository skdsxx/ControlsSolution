#region MyTabControl 文件信息
/***********************************************************
**文 件 名：MyTabControl 
**命名空间：WpfTest.Skins 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/4/16 16:19:00 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace TabControl.UserControls
{
    /// <summary>
    /// MyTabControl
    /// </summary>
    [TemplatePart(Name = "HeadPanel", Type = typeof(TabPanel))]//Tab标头模板
    public class TabControlEx : System.Windows.Controls.TabControl
    {
        public TabControlEx()
        {
            DefaultStyleKey = typeof(TabControlEx);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
