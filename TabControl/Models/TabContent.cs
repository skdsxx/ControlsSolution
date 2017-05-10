using TabControl.Pages;

namespace TabControl.Models
{
   public class TabContent
    {
        #region 属性

        /// <summary>
        /// 标题
        /// </summary>
        public string Header { get; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 是否可关闭
        /// </summary>
        public bool IsCanClosed { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public object Content { get; }

        #endregion

       public TabContent()
       {
            Header = "主页";
            Content = new MainPage();
            Icon = "/Images/home_page.png";
            IsSelected = true;
            IsCanClosed = false;
       }
        public TabContent(string header, object content)
        {
            Header = header;
            Content = content;
            IsSelected = true;
            IsCanClosed = true;
        }

        public TabContent(string header, string icoUrl, object content)
        {
            Header = header;
            Content = content;
            Icon = icoUrl;
            IsSelected = true;
            IsCanClosed = true;
        }
    }
}
