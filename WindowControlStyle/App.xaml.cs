using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WindowControlStyle
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        ///// <summary>
        ///// 当前使用的语言
        ///// </summary>
        //public static string Language { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //try
            //{
            //    var provider = TryFindResource("Lang") as XmlDataProvider;
            //    if (provider == null)
            //    {
            //        return;
            //    }
            //    Language = GetAppSetting("Language");

            //    switch (Language)
            //    {
            //        case "EN":
            //            provider.Source = new Uri(@"Languages\English.xml", UriKind.Relative);
            //            break;

            //        case "CN":
            //            provider.Source = new Uri(@"Languages\Chinese.xml", UriKind.Relative);
            //            break;

            //        default:
            //            provider.Source = new Uri(@"Languages\Chinese.xml", UriKind.Relative);
            //            break;
            //    }
            //    provider.Refresh();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("初始化系统语言发生了异常~");
            //}
        }
        /// <summary>
        /// 获取 AppSetting 值
        /// </summary>
        /// <param name="key">AppSetting key值</param>
        /// <returns></returns>
        public string GetAppSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return ConfigurationManager.AppSettings[key];
        }
    }
}
