using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WindowControlStyle.Skins
{
    /// <summary>
    /// 根据背景色获取前景色。当然也可反着用
    /// </summary>
    public class BackgroundToForegroundConverter : IValueConverter
    {
        private Color IdealTextColor(Color bg)
        {
            const int nThreshold = 105;
            var bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            var foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                var idealForegroundColor = this.IdealTextColor(((SolidColorBrush)value).Color);
                var foreGroundBrush = new SolidColorBrush(idealForegroundColor);
                foreGroundBrush.Freeze( );
                return foreGroundBrush;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    /// <summary>
    /// 百分比转换为角度值
    /// </summary>
    public class PercentToAngleConverter : IValueConverter
    {
        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            var percent = Double.Parse(value.ToString());
            if (percent >= 1) return 360.0D;
            return percent * 360;
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            throw new NotImplementedException( );
        }
    }

    /// <summary>
    /// 字符串转整数
    /// </summary>
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            return int.Parse(value.ToString( ));
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            throw new NotImplementedException( );
        }
    }

    /// <summary>
    /// 获取Thickness固定值double
    /// </summary>
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            var thickness = (Thickness)value;
            return thickness.Left;
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    /// <summary>
    /// 计算树节点的左缩进位置
    /// </summary>
    public class TreeViewMarginConverter : IValueConverter
    {
        public double Length { get; set; }

        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);
            int dep = this.GetDepth(item);
            return new Thickness(Length * dep , 0 , 0 , 0);
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        public int GetDepth(TreeViewItem item)
        {
            TreeViewItem parent;
            while ((parent = GetParent(item)) != null)
            {
                return GetDepth(parent) + 1;
            }
            return 0;
        }

        private TreeViewItem GetParent(TreeViewItem item)
        {
            var parent = item != null ? VisualTreeHelper.GetParent(item) : null;
            while (parent != null && !(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as TreeViewItem;
        }
    }

    /// <summary>
    /// 这是一个颠倒黑白的世界
    /// </summary>
    public sealed class TrueToFalseConverter : IValueConverter
    {
        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            var v = (bool)value;
            return !v;
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            throw new NotImplementedException( );
        }
    }

    /// <summary>
    /// 常用转换器的静态引用
    /// 使用实例：Converter={x:Static local:XConverter.TrueToFalseConverter}
    /// </summary>
    public sealed class XConverter
    {
        private static BooleanToVisibilityConverter myBooleanToVisibilityConverter;
        private static TrueToFalseConverter myTrueToFalseConverter;
        private static ThicknessToDoubleConverter myThicknessToDoubleConverter;
        private static BackgroundToForegroundConverter myBackgroundToForegroundConverter;
        private static TreeViewMarginConverter myTreeViewMarginConverter;
        private static PercentToAngleConverter myPercentToAngleConverter;

        static XConverter()
        {
            myBooleanToVisibilityConverter = new BooleanToVisibilityConverter( );
            myTrueToFalseConverter = new TrueToFalseConverter( );
            myThicknessToDoubleConverter = new ThicknessToDoubleConverter( );
            myBackgroundToForegroundConverter = new BackgroundToForegroundConverter( );
            myTreeViewMarginConverter = new TreeViewMarginConverter( );
            myPercentToAngleConverter = new PercentToAngleConverter( );
        }

        public static BooleanToVisibilityConverter BooleanToVisibilityConverter
        {
            get { return myBooleanToVisibilityConverter; }
        }

        public static TrueToFalseConverter TrueToFalseConverter
        {
            get { return myTrueToFalseConverter; }
        }

        public static ThicknessToDoubleConverter ThicknessToDoubleConverter
        {
            get { return myThicknessToDoubleConverter; }
        }
        public static BackgroundToForegroundConverter BackgroundToForegroundConverter
        {
            get { return myBackgroundToForegroundConverter; }
        }
        public static TreeViewMarginConverter TreeViewMarginConverter
        {
            get { return myTreeViewMarginConverter; }
        }

        public static PercentToAngleConverter PercentToAngleConverter
        {
            get { return myPercentToAngleConverter; }
        }
    }





}
