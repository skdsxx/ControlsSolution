#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: SUNXIAN
  * 命名空间: TabControl.UserControls
  * 文 件 名: MyTabControl
  * 创建人员: SunXian
  * 创建时间: 2017/5/11 14:24:01
  
//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TabControl.UserControls
{
    public class MyTabControl : UniformGrid
    {
        public MyTabControl()
        {
            this.IsItemsHost = true;
            this.Rows = 1;

            //Default, so not really needed..
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var totalMaxWidth = this.Children.OfType<TabItem>().Sum(tab => tab.MaxWidth);
            if (!double.IsInfinity(totalMaxWidth))
            {
                this.MinWidth = totalMaxWidth;
                this.HorizontalAlignment = (constraint.Width > totalMaxWidth)
                    ? HorizontalAlignment.Left
                    : HorizontalAlignment.Stretch;
            }

            return base.MeasureOverride(constraint);
        }
    }
}
