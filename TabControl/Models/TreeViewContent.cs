namespace TabControl.Models
{
    public class TreeViewContent
    {
        #region 属性
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visibility { get; set; }

        /// <summary>
        /// 菜单标识（用于识别要加载的TabItem内容）
        /// </summary>
        public object Flag { get; set; }

        public string Parent { get; set; }

        /// <summary>
        /// 子菜单项
        /// </summary>
       // public List<TreeViewContent> Children { get; set; }
        #endregion

        /// <summary>
        /// 初始化菜单项
        /// </summary>
        public TreeViewContent()
        {
            Header = "新建菜单";
            Icon = "../Images/home.png";
            IsEnabled = true;
            Visibility = true;
            IsExpanded = false;
            Flag = null;
            Parent = "";
            //Children = new List<TreeViewContent>();
        }
        /// <summary>
        /// 初始化菜单项
        /// </summary>
        /// <param name="header">菜单标题</param>
        /// <param name="icon">菜单图标</param>
        public TreeViewContent(string header, string icon)
        {
            Header = header;
            Icon = icon;
            IsEnabled = true;
            Visibility = true;
            IsExpanded = false;
            Flag = null;
            Parent = "系统设置"; //Children = new List<TreeViewContent>();
        }

        /// <summary>
        /// 初始化菜单项
        /// </summary>
        /// <param name="header">菜单标题</param>
        /// <param name="icon">菜单图标</param>
        /// <param name="parent"></param>
        /// <param name="flag">菜单标识（用于识别要加载的TabItem内容）</param>
        public TreeViewContent(string header, string icon,string parent, object flag = null)
        {
            Header = header;
            Icon = icon;
            Flag = flag;
            IsEnabled = true;
            Visibility = true;
            IsExpanded = false;
            Parent = parent;
            //Children = new List<TreeViewContent>();
        }
    }
}
