using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowControlStyle.Resources
{
    /// <summary>
    /// 国际化     注:语言资源文件在VS2010的属性设置   复制到输出目录:始终复制     生成操作:内容
    /// 资源文件 LanguageCode ,LanguageName, LanguageDisplayName,LanguageEnabled 字段必填
    /// </summary>
    public class LanguageInit
    {
        private static string _currentLanguage = "zh-cn";
        /// <summary>
        /// 设置或获取语言编码,如果设置失败,则可能语言资源文件错误
        /// </summary>
        public static string CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                if (UpdateCurrentLanguage(value))
                    _currentLanguage = value;
            }
        }

        private static List<Language> _languageIndex;
        /// <summary>
        /// 所有语言索引
        /// </summary>
        public static List<Language> LanguageIndex
        {
            get { return _languageIndex; }
        }

        /// <summary>
        /// 初始化,加载语言目录下的所有语言文件
        /// </summary>
        public static void Initialize()
        {
            _languageIndex = new List<Language>();
            string dirstring = AppDomain.CurrentDomain.BaseDirectory + "Resources\\Languages\\";
            DirectoryInfo directory = new DirectoryInfo(dirstring);
            FileInfo[] files = directory.GetFiles();
            foreach (var item in files)
            {
                Language language = new Language();
                ResourceDictionary rd = new ResourceDictionary();
                rd.Source = new Uri(item.FullName);
                language.LanguageCode = rd["LanguageCode"] == null ? "未知" : rd["LanguageCode"].ToString();
                language.LanguageName = rd["LanguageName"] == null ? "未知" : rd["LanguageName"].ToString();
                language.LanguageDisplayName = rd["LanguageDisplayName"] == null ? "未知" : rd["LanguageDisplayName"].ToString();
                language.LanguageEnabled = rd["LanguageEnabled"] == null ? false : bool.Parse(rd["LanguageEnabled"].ToString());
                language.Resourcefile = item.FullName;
                language.Resource = rd;
                if (language.LanguageEnabled)
                    _languageIndex.Add(language);
            }
        }

        /// <summary>
        /// 更新语言配置.  同时同步CurrentLanguage字段
        /// </summary>
        private static bool UpdateCurrentLanguage(string languageCode)
        {
            if (LanguageIndex.Exists(P => P.LanguageCode == languageCode && P.LanguageEnabled == true))
            {
                Language language = LanguageIndex.Find(P => P.LanguageCode == languageCode && P.LanguageEnabled == true);
                if (language != null)
                {
                    foreach (var item in LanguageIndex)
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(item.Resource);
                    }
                    Application.Current.Resources.MergedDictionaries.Add(language.Resource);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 查找语言资源文件具体的某项值,类似索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLanguageValue(string key)
        {
            ResourceDictionary rd = Application.Current.Resources;
            if (rd == null)
                return "";
            object obj = rd[key];
            return obj == null ? "" : obj.ToString();
        }
    }
}
