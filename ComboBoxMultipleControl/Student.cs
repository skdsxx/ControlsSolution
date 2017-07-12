#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: SUNXIAN
  * 命名空间: ComboBoxMultipleControl
  * 文 件 名: Student
  * 创建人员: SunXian
  * 创建时间: 2017/7/11 9:51:52
  
//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion

using System;

namespace ComboBoxMultipleControl
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
