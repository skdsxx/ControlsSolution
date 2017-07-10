#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: SUNXIAN
  * 命名空间: CanvasDragMove
  * 文 件 名: WindowApplicationSettings
  * 创建人员: SunXian
  * 创建时间: 2017/5/12 14:36:47
  
//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace CanvasDragMove
{
    public class WindowApplicationSettings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        public List<BtnModel> Models
        {
            get { return (List<BtnModel>)(this["Models"]); }
            set { this["Models"] = value; }
        }


        //[UserScopedSetting]
        //public String WinText
        //{
        //    get { return (String)this["WinText"]; }
        //    set { this["WinText"] = value; }
        //}


        //[UserScopedSetting]
        //[DefaultSettingValue("0, 0")]
        //public Point ButtonLocation
        //{
        //    get { return (Point)(this["ButtonLocation"]); }
        //    set { this["ButtonLocation"] = value; }
        //}


        //[UserScopedSetting]
        //[DefaultSettingValue("0, 0")]
        //public Point TextBoxLocation
        //{
        //    get { return (Point)(this["TextBoxLocation"]); }
        //    set { this["TextBoxLocation"] = value; }
        //}
        //[UserScopedSetting]
        //[DefaultSettingValue("0, 0")]
        //public Point LabelLocation
        //{
        //    get { return (Point)(this["LabelLocation"]); }
        //    set { this["LabelLocation"] = value; }
        //}
    }

    public class BtnModel
    {
        public string WinName { get; set; }
        public string WinCode { get; set; }

        [DefaultSettingValue("0.1, 0.1")]
        public Point ButtonLocation { get; set; }
    }
}
